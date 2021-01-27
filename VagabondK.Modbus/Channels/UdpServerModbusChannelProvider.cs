﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VagabondK.Modbus.Logging;

namespace VagabondK.Modbus.Channels
{
    public class UdpServerModbusChannelProvider : ModbusChannelProvider
    {
        public UdpServerModbusChannelProvider(int port)
        {
            Port = port;
            udpClient = new UdpClient(port);
        }

        public int Port { get; }

        private readonly UdpClient udpClient;
        private readonly Dictionary<string, WeakReference<UdpClientModbusChannel>> channels = new Dictionary<string, WeakReference<UdpClientModbusChannel>>();
        private CancellationTokenSource cancellationTokenSource;

        public override IReadOnlyList<ModbusChannel> Channels { get => channels.Values.Select(w => w.TryGetTarget(out var channel) ? channel : null).Where(c => c != null).ToList(); }

        public override string Description { get => udpClient.Client?.LocalEndPoint?.ToString(); }

        public override void Dispose()
        {
            lock (this)
            {
                if (!IsDisposed)
                {
                    IsDisposed = true;
                    udpClient.Close();
                }
            }
        }

        public override void Start()
        {
            lock (this)
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(nameof(TcpServerModbusChannelProvider));

                cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() =>
                {
                    while (!cancellationTokenSource.IsCancellationRequested)
                    {
                        IPEndPoint remoteEndPoint = null;
                        var received = udpClient.Receive(ref remoteEndPoint);

                        if (channels.TryGetValue(remoteEndPoint.ToString(), out var channelReference)
                            && channelReference.TryGetTarget(out var channel))
                        {
                            channel.AddReceivedMessage(received);
                        }
                        else
                        {
                            channel = new UdpClientModbusChannel(this, remoteEndPoint, received)
                            {
                                Logger = Logger
                            };
                            Logger?.Log(new ChannelOpenEventLog(channel));
                            channels[channel.Description] = new WeakReference<UdpClientModbusChannel>(channel);
                            RaiseCreatedEvent(new ModbusChannelCreatedEventArgs(channel));
                        }
                        foreach (var disposed in channels.Where(c => !c.Value.TryGetTarget(out var target)).Select(c => c.Key).ToArray())
                            channels.Remove(disposed);
                    }
                }, cancellationTokenSource.Token);
            }
        }

        public override void Stop()
        {
            lock (this)
            {
                cancellationTokenSource?.Cancel();
            }
        }

        class UdpClientModbusChannel : ModbusChannel
        {
            internal UdpClientModbusChannel(UdpServerModbusChannelProvider provider, IPEndPoint endPoint, byte[] received)
            {
                this.provider = provider;
                this.endPoint = endPoint;
                Description = endPoint.ToString();

                AddReceivedMessage(received);
            }

            private readonly UdpServerModbusChannelProvider provider;
            private readonly IPEndPoint endPoint;

            private readonly object writeLock = new object();
            private readonly object readLock = new object();
            private readonly Queue<byte> readBuffer = new Queue<byte>();
            private readonly EventWaitHandle readEventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

            internal void AddReceivedMessage(byte[] received)
            {
                foreach (var item in received)
                    readBuffer.Enqueue(item);
                readEventWaitHandle.Set();
            }

            public override bool IsDisposed { get; protected set; }

            public override string Description { get; protected set; }

            ~UdpClientModbusChannel()
            {
                Dispose();
            }

            public override void Dispose()
            {
                if (!IsDisposed)
                {
                    provider?.channels?.Remove(Description);
                    IsDisposed = true;
                }
            }

            private byte? GetByte(int timeout)
            {
                lock (readBuffer)
                {
                    if (readBuffer.Count > 0)
                        return readBuffer.Dequeue();
                    else
                        readEventWaitHandle.Reset();
                }

                if (timeout == 0 ? readEventWaitHandle.WaitOne() : readEventWaitHandle.WaitOne(timeout))
                    return readBuffer.Count > 0 ? readBuffer.Dequeue() : (byte?)null;
                else
                    return null;
            }

            public override void Write(byte[] bytes)
            {
                lock (writeLock)
                {
                    try
                    {
                        provider?.udpClient?.Send(bytes, bytes.Length, endPoint);
                    }
                    catch
                    {
                        throw new TimeoutException();
                    }
                }
            }

            public override byte Read(int timeout)
            {
                lock (readLock)
                {
                    return GetByte(timeout) ?? throw new TimeoutException();
                }
            }

            public override IEnumerable<byte> Read(uint count, int timeout)
            {
                lock (readLock)
                {
                    for (int i = 0; i < count; i++)
                    {
                        yield return GetByte(timeout) ?? throw new TimeoutException();
                    }
                }
            }

            public override IEnumerable<byte> ReadAllRemain()
            {
                lock (readLock)
                {
                    while (readBuffer.Count > 0)
                        yield return readBuffer.Dequeue();
                }
            }
        }

    }
}
