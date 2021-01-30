﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VagabondK.Modbus.Channels;
using VagabondK.Modbus.Data;
using VagabondK.Modbus.Logging;
using VagabondK.Modbus.Serialization;

namespace VagabondK.Modbus
{
    public static class ModbusMasterExtensions
    {
        public static bool[] ReadCoils(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length) => ReadCoils(modbusMaster, slaveAddress, address, length, modbusMaster.Timeout);
        public static bool[] ReadDiscreteInputs(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length) => ReadDiscreteInputs(modbusMaster, slaveAddress, address, length, modbusMaster.Timeout);
        public static ushort[] ReadHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length) => ReadHoldingRegisters(modbusMaster, slaveAddress, address, length, modbusMaster.Timeout);
        public static byte[] ReadHoldingRegisterBytes(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length) => ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, length, modbusMaster.Timeout);
        public static ushort[] ReadInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length) => ReadInputRegisters(modbusMaster, slaveAddress, address, length, modbusMaster.Timeout);
        public static byte[] ReadInputRegisterBytes(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length) => ReadInputRegisterBytes(modbusMaster, slaveAddress, address, length, modbusMaster.Timeout);
        public static void WriteCoils(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, IEnumerable<bool> values) => WriteCoils(modbusMaster, slaveAddress, address, values, modbusMaster.Timeout);
        public static void WriteHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, IEnumerable<ushort> values) => WriteHoldingRegisters(modbusMaster, slaveAddress, address, values, modbusMaster.Timeout);
        public static void WriteHoldingRegisterBytes(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, IEnumerable<byte> bytes) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, bytes, modbusMaster.Timeout);

        public static bool? ReadCoil(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadCoil(modbusMaster, slaveAddress, address, modbusMaster.Timeout);
        public static bool? ReadDiscreteInput(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadDiscreteInput(modbusMaster, slaveAddress, address, modbusMaster.Timeout);
        public static ushort? ReadHoldingRegister(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadHoldingRegister(modbusMaster, slaveAddress, address, modbusMaster.Timeout);
        public static ushort? ReadInputRegister(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadInputRegister(modbusMaster, slaveAddress, address, modbusMaster.Timeout);
        public static void WriteCoil(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool value) => WriteCoil(modbusMaster, slaveAddress, address, value, modbusMaster.Timeout);
        public static void WriteHoldingRegister(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort value) => WriteHoldingRegister(modbusMaster, slaveAddress, address, value, modbusMaster.Timeout);

        public static short ReadInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian) => ReadInt16FromInputRegisters(modbusMaster, slaveAddress, address, isBigEndian, modbusMaster.Timeout);
        public static ushort ReadUInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian) => ReadUInt16FromInputRegisters(modbusMaster, slaveAddress, address, isBigEndian, modbusMaster.Timeout);
        public static int ReadInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadInt32FromInputRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static uint ReadUInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadUInt32FromInputRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static long ReadInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadInt64FromInputRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static ulong ReadUInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadUInt64FromInputRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static float ReadSingleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadSingleFromInputRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static double ReadDoubleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadDoubleFromInputRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);

        public static short ReadInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadInt16FromInputRegisters(modbusMaster, slaveAddress, address, true, modbusMaster.Timeout);
        public static ushort ReadUInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadUInt16FromInputRegisters(modbusMaster, slaveAddress, address, true, modbusMaster.Timeout);
        public static int ReadInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadInt32FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static uint ReadUInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadUInt32FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static long ReadInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadInt64FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static ulong ReadUInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadUInt64FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static float ReadSingleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadSingleFromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static double ReadDoubleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadDoubleFromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);

        public static short ReadInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian) => ReadInt16FromHoldingRegisters(modbusMaster, slaveAddress, address, isBigEndian, modbusMaster.Timeout);
        public static ushort ReadUInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian) => ReadUInt16FromHoldingRegisters(modbusMaster, slaveAddress, address, isBigEndian, modbusMaster.Timeout);
        public static int ReadInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadInt32FromHoldingRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static uint ReadUInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadUInt32FromHoldingRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static long ReadInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadInt64FromHoldingRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static ulong ReadUInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadUInt64FromHoldingRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static float ReadSingleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadSingleFromHoldingRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);
        public static double ReadDoubleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian) => ReadDoubleFromHoldingRegisters(modbusMaster, slaveAddress, address, endian, modbusMaster.Timeout);

        public static short ReadInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadInt16FromHoldingRegisters(modbusMaster, slaveAddress, address, true, modbusMaster.Timeout);
        public static ushort ReadUInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadUInt16FromHoldingRegisters(modbusMaster, slaveAddress, address, true, modbusMaster.Timeout);
        public static int ReadInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadInt32FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static uint ReadUInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadUInt32FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static long ReadInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadInt64FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static ulong ReadUInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadUInt64FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static float ReadSingleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadSingleFromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);
        public static double ReadDoubleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address) => ReadDoubleFromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), modbusMaster.Timeout);

        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, short value, bool isBigEndian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, new ModbusEndian(isBigEndian).Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort value, bool isBigEndian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, new ModbusEndian(isBigEndian).Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int value, ModbusEndian endian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, uint value, ModbusEndian endian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, long value, ModbusEndian endian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ulong value, ModbusEndian endian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, float value, ModbusEndian endian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, double value, ModbusEndian endian) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), modbusMaster.Timeout);

        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, short value) => Write(modbusMaster, slaveAddress, address, value, true, modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort value) => Write(modbusMaster, slaveAddress, address, value, true, modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int value) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, uint value) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, long value) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ulong value) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, float value) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), modbusMaster.Timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, double value) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), modbusMaster.Timeout);


        public static bool[] ReadCoils(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.Coil, address, length), timeout) as ModbusReadBooleanResponse)?.Values?.ToArray();
        public static bool[] ReadDiscreteInputs(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.DiscreteInput, address, length), timeout) as ModbusReadBooleanResponse)?.Values?.ToArray();
        public static ushort[] ReadHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.HoldingRegister, address, length), timeout) as ModbusReadRegisterResponse)?.Values?.ToArray();
        public static byte[] ReadHoldingRegisterBytes(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.HoldingRegister, address, length), timeout) as ModbusReadRegisterResponse)?.Bytes?.ToArray();
        public static ushort[] ReadInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.InputRegister, address, length), timeout) as ModbusReadRegisterResponse)?.Values?.ToArray();
        public static byte[] ReadInputRegisterBytes(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort length, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.InputRegister, address, length), timeout) as ModbusReadRegisterResponse)?.Bytes?.ToArray();
        public static void WriteCoils(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, IEnumerable<bool> values, int timeout) => modbusMaster.Request(new ModbusWriteCoilRequest(slaveAddress, address, values), timeout);
        public static void WriteHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, IEnumerable<ushort> values, int timeout) => modbusMaster.Request(new ModbusWriteHoldingRegisterRequest(slaveAddress, address, values), timeout);
        public static void WriteHoldingRegisterBytes(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, IEnumerable<byte> bytes, int timeout) => modbusMaster.Request(new ModbusWriteHoldingRegisterRequest(slaveAddress, address, bytes), timeout);

        public static bool? ReadCoil(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.Coil, address, 1), timeout) as ModbusReadBooleanResponse)?.Values?.FirstOrDefault();
        public static bool? ReadDiscreteInput(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.DiscreteInput, address, 1), timeout) as ModbusReadBooleanResponse)?.Values?.FirstOrDefault();
        public static ushort? ReadHoldingRegister(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.HoldingRegister, address, 1), timeout) as ModbusReadRegisterResponse)?.Values?.FirstOrDefault();
        public static ushort? ReadInputRegister(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => (modbusMaster.Request(new ModbusReadRequest(slaveAddress, ModbusObjectType.InputRegister, address, 1), timeout) as ModbusReadRegisterResponse)?.Values?.FirstOrDefault();
        public static void WriteCoil(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool value, int timeout) => modbusMaster.Request(new ModbusWriteCoilRequest(slaveAddress, address, value), timeout);
        public static void WriteHoldingRegister(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort value, int timeout) => modbusMaster.Request(new ModbusWriteHoldingRegisterRequest(slaveAddress, address, value), timeout);

        public static short ReadInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian, int timeout) => BitConverter.ToInt16(new ModbusEndian(isBigEndian).Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 1, timeout)), 0);
        public static ushort ReadUInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian, int timeout) => BitConverter.ToUInt16(new ModbusEndian(isBigEndian).Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 1, timeout)), 0);
        public static int ReadInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout)  => BitConverter.ToInt32(endian.Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 2, timeout)), 0);
        public static uint ReadUInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToUInt32(endian.Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 2, timeout)), 0);
        public static long ReadInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToInt64(endian.Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 4, timeout)), 0);
        public static ulong ReadUInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToUInt64(endian.Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 4, timeout)), 0);
        public static float ReadSingleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToSingle(endian.Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 2, timeout)), 0);
        public static double ReadDoubleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToDouble(endian.Sort(ReadInputRegisterBytes(modbusMaster, slaveAddress, address, 4, timeout)), 0);

        public static short ReadInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadInt16FromInputRegisters(modbusMaster, slaveAddress, address, true, timeout);
        public static ushort ReadUInt16FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadUInt16FromInputRegisters(modbusMaster, slaveAddress, address, true, timeout);
        public static int ReadInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadInt32FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static uint ReadUInt32FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadUInt32FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static long ReadInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadInt64FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static ulong ReadUInt64FromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadUInt64FromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static float ReadSingleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadSingleFromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static double ReadDoubleFromInputRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadDoubleFromInputRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);

        public static short ReadInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian, int timeout) => BitConverter.ToInt16(new ModbusEndian(isBigEndian).Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 1, timeout)), 0);
        public static ushort ReadUInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, bool isBigEndian, int timeout) => BitConverter.ToUInt16(new ModbusEndian(isBigEndian).Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 1, timeout)), 0);
        public static int ReadInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToInt32(endian.Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 2, timeout)), 0);
        public static uint ReadUInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToUInt32(endian.Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 2, timeout)), 0);
        public static long ReadInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToInt64(endian.Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 4, timeout)), 0);
        public static ulong ReadUInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToUInt64(endian.Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 4, timeout)), 0);
        public static float ReadSingleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToSingle(endian.Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 2, timeout)), 0);
        public static double ReadDoubleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ModbusEndian endian, int timeout) => BitConverter.ToDouble(endian.Sort(ReadHoldingRegisterBytes(modbusMaster, slaveAddress, address, 4, timeout)), 0);

        public static short ReadInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadInt16FromHoldingRegisters(modbusMaster, slaveAddress, address, true, timeout);
        public static ushort ReadUInt16FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadUInt16FromHoldingRegisters(modbusMaster, slaveAddress, address, true, timeout);
        public static int ReadInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadInt32FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static uint ReadUInt32FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadUInt32FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static long ReadInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadInt64FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static ulong ReadUInt64FromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadUInt64FromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static float ReadSingleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadSingleFromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);
        public static double ReadDoubleFromHoldingRegisters(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int timeout) => ReadDoubleFromHoldingRegisters(modbusMaster, slaveAddress, address, new ModbusEndian(true), timeout);

        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, short value, bool isBigEndian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, new ModbusEndian(isBigEndian).Sort(BitConverter.GetBytes(value)), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort value, bool isBigEndian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, new ModbusEndian(isBigEndian).Sort(BitConverter.GetBytes(value)), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int value, ModbusEndian endian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, uint value, ModbusEndian endian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, long value, ModbusEndian endian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ulong value, ModbusEndian endian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, float value, ModbusEndian endian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, double value, ModbusEndian endian, int timeout) => WriteHoldingRegisterBytes(modbusMaster, slaveAddress, address, endian.Sort(BitConverter.GetBytes(value)), timeout);

        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, short value, int timeout) => Write(modbusMaster, slaveAddress, address, value, true, timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ushort value, int timeout) => Write(modbusMaster, slaveAddress, address, value, true, timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, int value, int timeout) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, uint value, int timeout) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, long value, int timeout) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, ulong value, int timeout) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, float value, int timeout) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), timeout);
        public static void Write(this ModbusMaster modbusMaster, byte slaveAddress, ushort address, double value, int timeout) => Write(modbusMaster, slaveAddress, address, value, new ModbusEndian(true), timeout);
    }
}