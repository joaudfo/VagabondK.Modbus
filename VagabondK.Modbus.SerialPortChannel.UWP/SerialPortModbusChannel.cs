﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VagabondK.Modbus.Logging;
using VagabondK.Modbus.Serialization;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace VagabondK.Modbus.Channels
{
    /// <summary>
    /// Serial 포트 Modbus 채널
    /// </summary>
    public class SerialPortModbusChannel : ModbusChannel
    {
        /// <summary>
        /// Serial 디바이스 감시 시작
        /// </summary>
        public static void StartDeviceWatcher()
        {
            //DeviceInformationCollection infoList = DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector()).AsTask().Result;

            //lock (_SerialDeviceIDs)
            //{
            //    foreach (var info in infoList)
            //        _SerialDeviceIDs.Add(info.Id);
            //}
            if (deviceWatcher == null)
            {
                deviceWatcher = DeviceInformation.CreateWatcher(SerialDevice.GetDeviceSelector());
                deviceWatcher.Added += DeviceWatcher_Added;
                deviceWatcher.Updated += DeviceWatcher_Updated;
                deviceWatcher.Removed += DeviceWatcher_Removed;
                deviceWatcher.Start();
            }
        }

        private static HashSet<string> serialDeviceIDs = new HashSet<string>();
        private static DeviceWatcher deviceWatcher = null;
        private static Dictionary<int, WeakReference<SerialPortModbusChannel>> instances = new Dictionary<int, WeakReference<SerialPortModbusChannel>>();

        private static void AddInstance(SerialPortModbusChannel channelSerial)
        {
            lock (instances)
            {
                instances[channelSerial.GetHashCode()] = new WeakReference<SerialPortModbusChannel>(channelSerial);
            }
        }

        private static void RemoveIntance(int hashCode)
        {
            lock (instances)
            {
                instances.Remove(hashCode);
            }
        }

        private static void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            lock (serialDeviceIDs)
            {
                serialDeviceIDs.Add(args.Id);
            }
            foreach (var instance in instances.Values.ToArray())
            {
                if (instance.TryGetTarget(out var channelSerial))
                {
                    channelSerial.Open(args.Id);
                }
            }
        }
        private static void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {

        }
        private static void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            lock (serialDeviceIDs)
            {
                serialDeviceIDs.Remove(args.Id);
            }
        }


        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="portName">포트 이름</param>
        /// <param name="baudRate">Baud Rate</param>
        /// <param name="dataBits">Data Bits</param>
        /// <param name="stopBits">Stop Bits</param>
        /// <param name="parity">Parity</param>
        /// <param name="handshake">ㅗandshake</param>
        public SerialPortModbusChannel(string portName, int baudRate, int dataBits,
            SerialStopBitCount stopBits, SerialParity parity, SerialHandshake handshake)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
            Handshake = handshake;

            Description = PortName;

            foreach (var instance in instances.ToArray())
            {
                if (!instance.Value.TryGetTarget(out var channelSerial))
                {
                    RemoveIntance(channelSerial.GetHashCode());
                }
            }


            Open(null);

            AddInstance(this);
        }

        /// <summary>
        /// 포트 이름
        /// </summary>
        public string PortName { get; }

        /// <summary>
        /// Baud Rate
        /// </summary>
        public int BaudRate { get; }

        /// <summary>
        /// Data Bits
        /// </summary>
        public int DataBits { get; }

        /// <summary>
        /// Stop Bits
        /// </summary>
        public SerialStopBitCount StopBits { get; }

        /// <summary>
        /// Parity
        /// </summary>
        public SerialParity Parity { get; }

        /// <summary>
        /// Handshake
        /// </summary>
        public SerialHandshake Handshake { get; }

        /// <summary>
        /// DTR 활성화 여부
        /// </summary>
        public bool DtrEnable { get => serialDevice.IsDataTerminalReadyEnabled; set => serialDevice.IsDataTerminalReadyEnabled = value; }

        /// <summary>
        /// RTS 활성화 여부
        /// </summary>
        public bool RtsEnable { get => serialDevice.IsRequestToSendEnabled; set => serialDevice.IsRequestToSendEnabled = value; }

        private SerialDevice serialDevice = null;
        private DataReader dataReader = null;
        private DataWriter dataWriter = null;

        private readonly object connectLock = new object();
        private readonly object writeLock = new object();
        private readonly object readLock = new object();
        private readonly Queue<byte> readBuffer = new Queue<byte>();
        private readonly EventWaitHandle readEventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        private bool isRunningReceive = false;

        /// <summary>
        /// 리소스 해제 여부
        /// </summary>
        public override bool IsDisposed { get; protected set; }

        /// <summary>
        /// 채널 설명
        /// </summary>
        public override string Description { get; protected set; }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~SerialPortModbusChannel()
        {
            Dispose();
        }

        /// <summary>
        /// 리소스 해제
        /// </summary>
        public override void Dispose()
        {
            IsDisposed = true;

            RemoveIntance(GetHashCode());

            dataReader?.Dispose();
            dataReader = null;
            dataWriter?.Dispose();
            dataWriter = null;
            serialDevice?.Dispose();
            serialDevice = null;
        }

        private void SetSerialDevice(SerialDevice serialDevice)
        {
            dataReader?.Dispose();
            dataReader = null;
            dataWriter?.Dispose();
            dataWriter = null;
            this.serialDevice?.Dispose();
            this.serialDevice = null;

            this.serialDevice = serialDevice;

            this.serialDevice.BaudRate = (uint)BaudRate;
            this.serialDevice.DataBits = (ushort)DataBits;
            this.serialDevice.StopBits = StopBits;
            this.serialDevice.Parity = Parity;
            this.serialDevice.Handshake = Handshake;

            dataReader = new DataReader(this.serialDevice.InputStream)
            {
                InputStreamOptions = InputStreamOptions.Partial
            };
            dataWriter = new DataWriter(this.serialDevice.OutputStream);
        }

        private void Open(string deviceId)
        {
            lock (connectLock)
            {
                if (serialDevice == null)
                {
                    try
                    {
                        if (deviceId == null)
                        {
                            lock (serialDeviceIDs)
                            {
                                foreach (var id in serialDeviceIDs)
                                {
                                    SerialDevice serialDevice = SerialDevice.FromIdAsync(id).AsTask().Result;
                                    if (serialDevice?.PortName == PortName)
                                    {
                                        SetSerialDevice(serialDevice);
                                        break;
                                    }
                                    else
                                    {
                                        serialDevice?.Dispose();
                                    }
                                }
                            }
                        }
                        else
                        {
                            SerialDevice serialDevice = SerialDevice.FromIdAsync(deviceId).AsTask().Result;
                            if (serialDevice?.PortName == PortName)
                            {
                                SetSerialDevice(serialDevice);
                            }
                            else
                            {
                                serialDevice?.Dispose();
                            }
                        }
                    }
                    catch
                    {
                        dataReader?.Dispose();
                        dataReader = null;
                        dataWriter?.Dispose();
                        dataWriter = null;
                        serialDevice?.Dispose();
                        serialDevice = null;
                    }
                }
            }
        }

        private byte? GetByte(int timeout)
        {
            lock (readBuffer)
            {
                if (readBuffer.Count == 0)
                {
                    readEventWaitHandle.Reset();

                    Task.Run(async () =>
                    {
                        if (!isRunningReceive)
                        {
                            isRunningReceive = true;

                            try
                            {
                                if (dataReader != null)
                                {
                                    while (true)
                                    {
                                        var received = await dataReader.LoadAsync(1);
                                        if (received >= 0)
                                        {
                                            readBuffer.Enqueue(dataReader.ReadByte());
                                            readEventWaitHandle.Set();
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                            readEventWaitHandle.Set();
                            isRunningReceive = false;
                        }
                    });
                }
                else return readBuffer.Dequeue();
            }

            if (timeout == 0 ? readEventWaitHandle.WaitOne() : readEventWaitHandle.WaitOne(timeout))
                return readBuffer.Count > 0 ? readBuffer.Dequeue() : (byte?)null;
            else
                return null;

        }

        /// <summary>
        /// 바이트 배열 쓰기
        /// </summary>
        /// <param name="bytes">바이트 배열</param>
        public override void Write(byte[] bytes)
        {
            lock (writeLock)
            {
                if (dataWriter != null)
                {
                    try
                    {
                        dataWriter?.WriteBytes(bytes);
                        dataWriter.StoreAsync().AsTask().Wait();
                    }
                    catch
                    {
                        throw new TimeoutException();
                    }
                }
                else
                {
                    throw new TimeoutException();
                }
            }
        }

        /// <summary>
        /// 1 바이트 읽기
        /// </summary>
        /// <param name="timeout">제한시간(밀리초)</param>
        /// <returns>읽은 바이트</returns>
        public override byte Read(int timeout)
        {
            lock (readLock)
            {
                return GetByte(timeout) ?? throw new TimeoutException();
            }
        }

        /// <summary>
        /// 여러 개의 바이트 읽기
        /// </summary>
        /// <param name="count">읽을 개수</param>
        /// <param name="timeout">제한시간(밀리초)</param>
        /// <returns>읽은 바이트 열거</returns>
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

        /// <summary>
        /// 채널에 남아있는 모든 바이트 읽기
        /// </summary>
        /// <returns>읽은 바이트 열거</returns>
        public override IEnumerable<byte> ReadAllRemain()
        {
            lock (readLock)
            {
                while (readBuffer.Count > 0)
                    yield return readBuffer.Dequeue();
                if (dataReader.UnconsumedBufferLength > 0)
                    dataReader.ReadBuffer(dataReader.UnconsumedBufferLength);
            }
        }
    }
}