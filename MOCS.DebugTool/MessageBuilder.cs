using System.Buffers.Binary;
using MOCS.Coms;
using MOCS.Cores.MCU;
using MOCS.Protocals;
using MOCS.Protocals.Propulsion.MCUToMOCS;
using MOCS.Protocals.VehicleControl.VehicleToMOCS;

namespace MOCS.DebugTool;

/// <summary>
/// 报文构造器 —— 按协议一对一复刻 MOCS ↔ MCU / MOCS ↔ VCU 所有报文
/// 每种报文使用对应的数据内容类 ToByteArray() 构造 UserData，
/// 再通过 BaseSendMsg + MessageFactory 封装为完整传输帧。
/// </summary>
public static class MessageBuilder
{
    private static readonly MessageFactory<BaseMessage> _factory = new();
    private static ushort _seqCounter = 1;
    private static ushort NextSeq() => _seqCounter++;

    // ================================================================
    // 公共工具方法
    // ================================================================

    /// <summary>
    /// 构造完整传输帧（8字节头 + UserData + CRC + 帧头帧尾）
    /// </summary>
    private static byte[] BuildFrame(byte msgId, ReadOnlySpan<byte> userData)
    {
        var msg = new BaseSendMsg
        {
            SequenceNumber = NextSeq(),
            RepeatCounter = 0x00,
            Destination = 0x01,
            Source = 0x01,
            PartId = 0x01,
            MsgId = msgId,
            UserData = userData.ToArray(),
        };
        var payload = msg.ToByteArray();
        return _factory.ToTransmitByteArray(payload);
    }

    /// <summary>
    /// 生成指定长度的全零字节数组
    /// </summary>
    public static byte[] GenerateZeroUserData(int length) => new byte[length];

    /// <summary>
    /// 生成指定长度的随机字节数组
    /// </summary>
    public static byte[] GenerateRandomBytes(int length)
    {
        byte[] data = new byte[length];
        Random.Shared.NextBytes(data);
        return data;
    }

    // ================================================================
    // DCS → MCU 下行报文（9种）
    // ================================================================

    /// <summary>
    /// 1. DCS状态报文 (MOCSStatusMsg, MsgId=0x01, UserData=148字节)
    /// </summary>
    public static byte[] BuildDCSStatusMsg()
    {
        var field = MOCSStatus.Instance;
        return BuildFrame(0x01, field.ToByteArray());
    }

    /// <summary>
    /// 1. DCS状态报文 (使用指定的 UserData 字节，用于随机数据发送)
    /// </summary>
    public static byte[] BuildDCSStatusMsg(byte[] userData148)
    {
        if (userData148.Length != 148)
            throw new ArgumentException($"DCS状态报文 UserData 必须为 148 字节，实际: {userData148.Length}");
        return BuildFrame(0x01, userData148);
    }

    /// <summary>
    /// 2. 改变牵引状态报文 (ChangeTractionStateMsg, MsgId=0x02, UserData=2字节)
    /// </summary>
    public static byte[] BuildChangeTractionStateMsg(ChangeTractionState field)
    {
        return BuildFrame(0x02, field.ToByteArray());
    }

    /// <summary>
    /// 2. 改变牵引状态报文 (使用指定的 UserData 字节)
    /// </summary>
    public static byte[] BuildChangeTractionStateMsg(byte[] userData2)
    {
        if (userData2.Length != 2)
            throw new ArgumentException($"改变牵引状态报文 UserData 必须为 2 字节，实际: {userData2.Length}");
        return BuildFrame(0x02, userData2);
    }

    /// <summary>
    /// 3. 悬浮架登录报文 (LogInMaglevVehicleMsg, MsgId=0x03, UserData=14字节)
    /// </summary>
    public static byte[] BuildLogInMaglevVehicleMsg(LogInMaglevVehicle field)
    {
        return BuildFrame(0x03, field.ToByteArray());
    }

    /// <summary>
    /// 3. 悬浮架登录报文 (使用指定的 UserData 字节)
    /// </summary>
    public static byte[] BuildLogInMaglevVehicleMsg(byte[] userData14)
    {
        if (userData14.Length != 14)
            throw new ArgumentException($"悬浮架登录报文 UserData 必须为 14 字节，实际: {userData14.Length}");
        return BuildFrame(0x03, userData14);
    }

    /// <summary>
    /// 4. 悬浮架注销报文 (LogOutMaglevVehicleMsg, MsgId=0x04, UserData=2字节)
    /// </summary>
    public static byte[] BuildLogOutMaglevVehicleMsg(LogOutMaglevVehicle field)
    {
        return BuildFrame(0x04, field.ToByteArray());
    }

    /// <summary>
    /// 4. 悬浮架注销报文 (使用指定的 UserData 字节)
    /// </summary>
    public static byte[] BuildLogOutMaglevVehicleMsg(byte[] userData2)
    {
        if (userData2.Length != 2)
            throw new ArgumentException($"悬浮架注销报文 UserData 必须为 2 字节，实际: {userData2.Length}");
        return BuildFrame(0x04, userData2);
    }

    /// <summary>
    /// 5. 最大速度曲线报文 (MaxSpeedCurveMsg, MsgId=0x05, UserData=16字节)
    /// </summary>
    public static byte[] BuildMaxSpeedCurveMsg(byte[] userData16)
    {
        if (userData16.Length != 16)
            throw new ArgumentException($"最大速度曲线报文 UserData 必须为 16 字节，实际: {userData16.Length}");
        return BuildFrame(0x05, userData16);
    }

    /// <summary>
    /// 6. 线路数据报文 (TrackDataMsg, MsgId=0x06, UserData=变长)
    /// </summary>
    public static byte[] BuildTrackDataMsg(byte[] userData)
    {
        if (userData.Length % 2 != 0)
            throw new ArgumentException($"线路数据报文 UserData 长度必须为偶数，实际: {userData.Length}");
        return BuildFrame(0x06, userData);
    }

    /// <summary>
    /// 7. 线路数据删除报文 (RemoveTrackDataMsg, MsgId=0x07, UserData=8字节)
    /// </summary>
    public static byte[] BuildRemoveTrackDataMsg(RemoveTrackData field)
    {
        return BuildFrame(0x07, field.ToByteArray());
    }

    /// <summary>
    /// 7. 线路数据删除报文 (使用指定的 UserData 字节)
    /// </summary>
    public static byte[] BuildRemoveTrackDataMsg(byte[] userData8)
    {
        if (userData8.Length != 8)
            throw new ArgumentException($"线路数据删除报文 UserData 必须为 8 字节，实际: {userData8.Length}");
        return BuildFrame(0x07, userData8);
    }

    /// <summary>
    /// 8. 停车点运行状态请求报文 (RequestParkingPointStatusMsg, MsgId=0x08, UserData=4字节)
    /// </summary>
    public static byte[] BuildRequestParkingPointStatusMsg(RequestParkingPointStatus field)
    {
        return BuildFrame(0x08, field.ToByteArray());
    }

    /// <summary>
    /// 8. 停车点运行状态请求报文 (使用指定的 UserData 字节)
    /// </summary>
    public static byte[] BuildRequestParkingPointStatusMsg(byte[] userData4)
    {
        if (userData4.Length != 4)
            throw new ArgumentException($"停车点运行状态请求报文 UserData 必须为 4 字节，实际: {userData4.Length}");
        return BuildFrame(0x08, userData4);
    }

    /// <summary>
    /// 9. 停车点运行步进报文 (StepParkingPointMsg, MsgId=0x09, UserData=4字节)
    /// </summary>
    public static byte[] BuildStepParkingPointMsg(StepParkingPoint field)
    {
        return BuildFrame(0x09, field.ToByteArray());
    }

    /// <summary>
    /// 9. 停车点运行步进报文 (使用指定的 UserData 字节)
    /// </summary>
    public static byte[] BuildStepParkingPointMsg(byte[] userData4)
    {
        if (userData4.Length != 4)
            throw new ArgumentException($"停车点运行步进报文 UserData 必须为 4 字节，实际: {userData4.Length}");
        return BuildFrame(0x09, userData4);
    }

    // ================================================================
    // MCU → DCS 上行报文（2种）
    // ================================================================

    /// <summary>
    /// 10. 牵引状态报文 (MCUStatusMsg, MsgId=0x81, UserData=164字节)
    /// </summary>
    public static byte[] BuildMCUStatusMsg(byte[] userData164)
    {
        if (userData164.Length != 164)
            throw new ArgumentException($"MCUStatusMsg UserData 必须为 164 字节，实际: {userData164.Length}");
        return BuildFrame(0x81, userData164);
    }

    /// <summary>
    /// 11. 应答报文 (MCUReplyMsg, MsgId=0x82, UserData=12字节)
    /// </summary>
    public static byte[] BuildMCUReplyMsg(byte[] userData12)
    {
        if (userData12.Length != 12)
            throw new ArgumentException($"MCUReplyMsg UserData 必须为 12 字节，实际: {userData12.Length}");
        return BuildFrame(0x82, userData12);
    }

    // ================================================================
    // VCU 侧报文（保持兼容）
    // ================================================================

    /// <summary>
    /// EMSStatusMsgA 传输帧 (MsgId=0x81, UserData[0]=CANID 范围 0x21-0x34, UserData=10字节)
    /// </summary>
    public static byte[] BuildEMSStatusMsgA(byte canId, byte[] canData8)
    {
        if (canId < 0x21 || canId > 0x34)
            throw new ArgumentException($"EMSStatusMsgA CANID 必须在 0x21-0x34 范围，实际: 0x{canId:X2}");
        if (canData8.Length != 8)
            throw new ArgumentException($"CAN数据必须为 8 字节，实际: {canData8.Length}");

        byte[] userData = new byte[10];
        userData[0] = canId;
        Buffer.BlockCopy(canData8, 0, userData, 1, 8);
        // userData[9] = 0 (补齐偶数长度)
        return BuildFrame(0x81, userData);
    }

    /// <summary>
    /// EMSStatusMsgB 传输帧 (MsgId=0x82, UserData[0]=CANID 范围 0x61-0x74, UserData=10字节)
    /// </summary>
    public static byte[] BuildEMSStatusMsgB(byte canId, byte[] canData8)
    {
        if (canId < 0x61 || canId > 0x74)
            throw new ArgumentException($"EMSStatusMsgB CANID 必须在 0x61-0x74 范围，实际: 0x{canId:X2}");
        if (canData8.Length != 8)
            throw new ArgumentException($"CAN数据必须为 8 字节，实际: {canData8.Length}");

        byte[] userData = new byte[10];
        userData[0] = canId;
        Buffer.BlockCopy(canData8, 0, userData, 1, 8);
        // userData[9] = 0 (补齐偶数长度)
        return BuildFrame(0x82, userData);
    }

    /// <summary>
    /// VSPSStatusMsg 传输帧 (MsgId=0xE1, UserData[0]=CANID 0x5F, UserData=14字节)
    /// </summary>
    public static byte[] BuildVSPSStatusMsg(byte[] userData12)
    {
        if (userData12.Length != 12)
            throw new ArgumentException($"VSPSStatusMsg UserData 必须为 12 字节，实际: {userData12.Length}");

        byte[] userData = new byte[14];
        userData[0] = 0x5F;
        Buffer.BlockCopy(userData12, 0, userData, 1, 12);
        // userData[13] = 0 (补齐偶数长度)
        return BuildFrame(0xE1, userData);
    }

    /// <summary>
    /// OBCMsg 上行状态传输帧 (MsgId=0xF1, UserData=16字节)
    /// </summary>
    public static byte[] BuildOBCMsg(byte[] userData16)
    {
        if (userData16.Length != 16)
            throw new ArgumentException($"OBCMsg UserData 必须为 16 字节，实际: {userData16.Length}");
        return BuildFrame(0xF1, userData16);
    }

    // ================================================================
    // MOCS → VCU 下行报文（EMS 控制 / OBC 控制）
    // ================================================================

    /// <summary>
    /// EMSControlMsg 传输帧 (MsgId=0x21, UserData=8字节 CAN 数据)
    /// MOCS → LCU/GCU 的控制报文
    /// </summary>
    public static byte[] BuildEMSControlMsg(byte[] canData8)
    {
        if (canData8.Length != 8)
            throw new ArgumentException($"EMSControlMsg CAN数据必须为 8 字节，实际: {canData8.Length}");
        return BuildFrame(0x21, canData8);
    }

    /// <summary>
    /// OBCControlMsg 传输帧 (MsgId=0x71, UserData=14字节)
    /// MOCS → OBC 的控制报文
    /// </summary>
    public static byte[] BuildOBCControlMsg(byte[] userData14)
    {
        if (userData14.Length != 14)
            throw new ArgumentException($"OBCControlMsg UserData 必须为 14 字节，实际: {userData14.Length}");
        return BuildFrame(0x71, userData14);
    }

    // ================================================================
    // 针对性随机 UserData 生成
    // ================================================================

    /// <summary>
    /// 随机 MOCSStatus UserData (148字节，基于 MOCSStatus 字段结构)
    /// </summary>
    public static byte[] GenerateRandomMOCSStatus()
    {
        Span<byte> data = stackalloc byte[148];
        Random r = Random.Shared;

        data[0] = (byte)(r.Next(0, 4));          // Channel1RecvStatus
        data[2] = (byte)(r.Next(0, 4));          // Channel2RecvStatus
        data[4] = (byte)(r.Next(0, 3));          // MCUStatusMessageRecvStatus
        data[5] = (byte)(r.Next(0, 4));          // RequestForMCUStatusChangeReady
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(6, 2), (ushort)r.Next(0, 65535));
        data[8] = (byte)r.Next(0, 10);           // MaxNumForRepeat
        data[9] = (byte)r.Next(1, 255);          // CurrentMaglevVehicleIdentifier
        data[10] = (byte)r.Next(0, 16);           // LeviCommand + LeviStatus
        data[11] = (byte)r.Next(0, 8);            // SpeedType + Direction
        BinaryPrimitives.WriteInt16LittleEndian(data.Slice(12, 2), (short)r.Next(0, 1000));
        BinaryPrimitives.WriteInt16LittleEndian(data.Slice(14, 2), (short)r.Next(0, 200));
        BinaryPrimitives.WriteInt16LittleEndian(data.Slice(16, 2), (short)r.Next(0, 50));
        data[18] = (byte)r.Next(0, 8);           // DiagnosticInfo

        return data.ToArray();
    }

    /// <summary>
    /// 随机 ChangeTractionState UserData (2字节)
    /// </summary>
    public static byte[] GenerateRandomChangeTractionState()
    {
        Span<byte> data = stackalloc byte[2];
        data[0] = (byte)Random.Shared.Next(0, 8); // TargetState 枚举值
        return data.ToArray();
    }

    /// <summary>
    /// 随机 LogInMaglevVehicle UserData (14字节)
    /// </summary>
    public static byte[] GenerateRandomLogInMaglevVehicle()
    {
        Span<byte> data = stackalloc byte[14];
        Random r = Random.Shared;

        data[0] = (byte)r.Next(1, 255);           // MaglevVehicleIdentifier
        data[1] = (byte)r.Next(0, 4);             // MaglevVehicleType
        data[2] = (byte)r.Next(1, 255);           // MOCSID
        data[3] = (byte)r.Next(0, 4);             // MaglevVehicleDirection
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(4, 4), r.Next(0, 100000));
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(8, 4), r.Next(1000, 5000));
        BinaryPrimitives.WriteInt16LittleEndian(data.Slice(12, 2), (short)r.Next(100, 500));

        return data.ToArray();
    }

    /// <summary>
    /// 随机 LogOutMaglevVehicle UserData (2字节)
    /// </summary>
    public static byte[] GenerateRandomLogOutMaglevVehicle()
    {
        Span<byte> data = stackalloc byte[2];
        data[0] = (byte)Random.Shared.Next(1, 255); // MaglevVehicleIdentifier
        return data.ToArray();
    }

    /// <summary>
    /// 随机 RemoveTrackData UserData (8字节)
    /// </summary>
    public static byte[] GenerateRandomRemoveTrackData()
    {
        Span<byte> data = stackalloc byte[8];
        Random r = Random.Shared;

        BinaryPrimitives.WriteUInt16LittleEndian(data[..2], (ushort)r.Next(0, 65535));
        data[2] = (byte)r.Next(0, 4);             // RemoveFlag
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(3, 4), r.Next(0, 100000));

        return data.ToArray();
    }

    /// <summary>
    /// 随机 RequestParkingPointStatus UserData (4字节)
    /// </summary>
    public static byte[] GenerateRandomRequestParkingPointStatus()
    {
        Span<byte> data = stackalloc byte[4];
        Random r = Random.Shared;

        BinaryPrimitives.WriteUInt16LittleEndian(data[..2], (ushort)r.Next(0, 65535));
        data[2] = (byte)r.Next(1, 255);

        return data.ToArray();
    }

    /// <summary>
    /// 随机 StepParkingPoint UserData (4字节)
    /// </summary>
    public static byte[] GenerateRandomStepParkingPoint()
    {
        Span<byte> data = stackalloc byte[4];
        Random r = Random.Shared;

        BinaryPrimitives.WriteUInt16LittleEndian(data[..2], (ushort)r.Next(0, 65535));
        data[2] = (byte)r.Next(1, 255);

        return data.ToArray();
    }

    /// <summary>
    /// 随机 MCUStatus UserData (164字节，基于 MCUStatus 字段结构)
    /// </summary>
    public static byte[] GenerateRandomMCUStatus()
    {
        Span<byte> data = stackalloc byte[164];
        Random r = Random.Shared;

        data[0] = (byte)(r.Next(0, 4));            // Channel1RecvStatus
        data[2] = (byte)(r.Next(0, 4));            // Channel2RecvStatus
        data[4] = (byte)r.Next(0, 3);              // MCUSendReason
        data[5] = (byte)r.Next(0, 10);             // MsgNumToRepeat
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(6, 2), (ushort)r.Next(0, 65535));
        data[8] = (byte)(r.Next(0, 8));            // MCUStatusChangeReadinessInfo
        data[9] = (byte)r.Next(1, 255);            // CurrentMaglevVehicleIdentifier
        data[10] = (byte)(r.Next(0, 4));           // ClearCurrentMaglevVehicleReadinessInfo
        data[11] = (byte)r.Next(0, 255);           // DCSNum
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(12, 4), r.Next(0, 100000));
        BinaryPrimitives.WriteInt16LittleEndian(data.Slice(16, 2), (short)r.Next(0, 1000));
        BinaryPrimitives.WriteInt16LittleEndian(data.Slice(18, 2), (short)r.Next(0, 200));
        data[20] = (byte)r.Next(0, 101);           // TractionCapacityForCurrentVehicle
        data[21] = (byte)(r.Next(0, 8));           // CurrentMaglevVehicleSPRStatus
        data[22] = (byte)r.Next(1, 255);           // VirtualMaglevVehicleIdentifier
        data[23] = (byte)(r.Next(0, 4));           // ClearVirtualMaglevVehicleReadinessInfo
        data[24] = (byte)r.Next(0, 101);           // TractionCapacityForVirtualVehicle
        data[25] = (byte)r.Next(0, 6);             // MCUFaultStatus
        data[26] = (byte)r.Next(0, 6);             // ParkingPointsNum

        // 填充停车点调试信息（字节 27+）
        for (int i = 27; i < 164; i++)
            data[i] = (byte)r.Next(0, 256);

        return data.ToArray();
    }

    /// <summary>
    /// 随机 MCUReply UserData (12字节，基于 MCUReply 字段结构)
    /// </summary>
    public static byte[] GenerateRandomMCUReply()
    {
        Span<byte> data = stackalloc byte[12];
        Random r = Random.Shared;

        data[0] = (byte)(r.Next(0, 4));            // Channel1RecvStatus
        data[2] = (byte)(r.Next(0, 4));            // Channel2RecvStatus
        data[4] = (byte)r.Next(0, 8);              // MCUReplyErrorIdentifier
        data[5] = (byte)r.Next(0, 10);             // MsgNumToRepeat
        data[6] = (byte)r.Next(0, 4);              // MCUProcessStatus
        data[7] = (byte)r.Next(0x01, 0x10);        // ResponseMsgId

        return data.ToArray();
    }

    // ================================================================
    // VCU → MOCS 上行报文随机生成（EMS / VSPS / OBC）
    // ================================================================

    /// <summary>
    /// 随机 EMSStatusMsgA CAN数据 (8字节，含 CANID 共9字节)
    /// CANID 固定为 0x21 (LCU1)
    /// </summary>
    public static byte[] GenerateRandomEMSStatusMsgA()
    {
        Span<byte> data = stackalloc byte[9];
        Random r = Random.Shared;

        data[0] = 0x21;  // CANID = LCU1
        // Byte 0: GapSensors(3b) | EMSCmd(1b) | Life(4b)
        data[1] = (byte)((r.Next(0, 8) << 5) | (r.Next(0, 2) << 4) | r.Next(0, 16));
        // Byte 1: EMSSysStatus(1b) | Overload(1b) | AccSensors(2b) | Operation(1b) | Warning(1b) | Fault(2b)
        data[2] = (byte)((r.Next(0, 2) << 7) | (r.Next(0, 2) << 6) | (r.Next(0, 4) << 4) | (r.Next(0, 2) << 3) | (r.Next(0, 2) << 2) | r.Next(0, 4));
        // Byte 2: KM2Status(1b) | Temp(7b)
        data[3] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 3: KM1Contact(1b) | Gap(7b)
        data[4] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 4: KM2Contact(1b) | U(7b)
        data[5] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 5: CPUStatus(1b) | I1(7b)
        data[6] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 6: KM1Status(1b) | Acc(7b)
        data[7] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 7: Brake(1b) | SysSwitch(1b) | GapWarn(1b) | OverloadWarn(1b) | Stability(1b) | Cut(1b)
        data[8] = (byte)((r.Next(0, 2) << 5) | (r.Next(0, 2) << 4) | (r.Next(0, 2) << 3) | (r.Next(0, 2) << 2) | (r.Next(0, 2) << 1) | r.Next(0, 2));

        return data.ToArray();
    }

    /// <summary>
    /// 随机 EMSStatusMsgB CAN数据 (8字节，含 CANID 共9字节)
    /// CANID 固定为 0x61 (LCU1)
    /// </summary>
    public static byte[] GenerateRandomEMSStatusMsgB()
    {
        Span<byte> data = stackalloc byte[9];
        Random r = Random.Shared;

        data[0] = 0x61;  // CANID = LCU1
        // Byte 1: GapSensor1(1b) | Gap1(7b)
        data[1] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 2: GapSensor2(1b) | Gap2(7b)
        data[2] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 3: GapSensor3(1b) | Gap3(7b)
        data[3] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 4: GapSensor4(1b) | Gap4(7b)
        data[4] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 5: AccSensor1(1b) | Acc1(7b)
        data[5] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 6: AccSensor2(1b) | Acc2(7b)
        data[6] = (byte)((r.Next(0, 2) << 7) | r.Next(0, 128));
        // Byte 7: I2(7b)
        data[7] = (byte)r.Next(0, 128);
        // Byte 8: 保留
        data[8] = 0;

        return data.ToArray();
    }

    /// <summary>
    /// 随机 VSPSStatusMsg UserData (12字节)
    /// </summary>
    public static byte[] GenerateRandomVSPSStatus()
    {
        Span<byte> data = stackalloc byte[12];
        Random r = Random.Shared;

        // Byte 0-1: Life (UInt16 BigEndian)
        BinaryPrimitives.WriteUInt16BigEndian(data[..2], (ushort)r.Next(0, 65535));
        // Byte 2: 保留
        data[2] = 0;
        // Byte 3: Forward (0x00=反向, 0x01=正向)
        data[3] = (byte)r.Next(0, 2);
        // Byte 4-5: RelativePos (UInt16 BigEndian)
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(4, 2), (ushort)r.Next(0, 65535));
        // Byte 6-7: Speed (UInt16 BigEndian)
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(6, 2), (ushort)r.Next(0, 30000));
        // Byte 8-11: 保留
        for (int i = 8; i < 12; i++)
            data[i] = 0;

        return data.ToArray();
    }

    /// <summary>
    /// 随机 OBCMsg UserData (16字节)
    /// </summary>
    public static byte[] GenerateRandomOBCStatus()
    {
        Span<byte> data = stackalloc byte[16];
        Random r = Random.Shared;

        // 前 8 字节是 BitArray 编码的状态位
        data[0] = (byte)r.Next(0, 256);
        data[1] = (byte)r.Next(0, 256);
        data[2] = (byte)r.Next(0, 256);
        data[3] = (byte)r.Next(0, 256);
        data[4] = (byte)r.Next(0, 256);
        data[5] = (byte)r.Next(0, 256);
        data[6] = (byte)r.Next(0, 256);
        data[7] = (byte)r.Next(0, 256);
        // Byte 8-9: Battery440VCapacity (Int16 BigEndian)
        BinaryPrimitives.WriteInt16BigEndian(data.Slice(8, 2), (short)r.Next(0, 100));
        // Byte 10-11: Battery110VCapacity (Int16 BigEndian)
        BinaryPrimitives.WriteInt16BigEndian(data.Slice(10, 2), (short)r.Next(0, 100));
        // Byte 12-14: 保留
        for (int i = 12; i < 15; i++)
            data[i] = 0;
        // Byte 15: BrakeLevel
        data[15] = (byte)r.Next(0, 8);

        return data.ToArray();
    }

    // ================================================================
    // MOCS → VCU 下行报文随机生成（EMS 控制 / OBC 控制）
    // ================================================================

    /// <summary>
    /// 随机 EMSControlMsg CAN数据 (8字节)
    /// 基于 EMSControl 字段结构：TrainDirection | Life | ETStatus | EBStatus | Velocity | EMSCmd | BrakeLevel | Distance | CutCmd | ControllerId
    /// </summary>
    public static byte[] GenerateRandomEMSControl()
    {
        Span<byte> data = stackalloc byte[8];
        Random r = Random.Shared;

        // Byte 0: TrainDirection(高2位) | Life(低4位)
        data[0] = (byte)((r.Next(0, 2) << 6) | r.Next(0, 16));
        // Byte 1: ETStatus(bit 1) | EBStatus(bit 0)
        data[1] = (byte)((r.Next(0, 2) << 1) | r.Next(0, 2));
        // Byte 2: Velocity
        data[2] = (byte)r.Next(0, 200);
        // Byte 3: EMSCmd(高3位) | ControllerId(低5位)
        data[3] = (byte)((r.Next(0, 6) << 5) | r.Next(0, 6));
        // Byte 4: BrakeLevel(高3位) | ControllerId(低5位)
        data[4] = (byte)((r.Next(0, 8) << 5) | r.Next(0, 6));
        // Byte 5-6: Distance (UInt16 BigEndian)
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(5, 2), (ushort)r.Next(0, 50000));
        // Byte 7: CutCmd(高3位) | ControllerId(低5位)
        data[7] = (byte)((r.Next(0, 4) << 5) | r.Next(0, 6));

        return data.ToArray();
    }

    /// <summary>
    /// 随机 OBCControlMsg UserData (14字节)
    /// 基于 OBCControl 字段结构：BitArray(40bit=5字节) + 保留(8字节) + BrakeLevel(1字节)
    /// </summary>
    public static byte[] GenerateRandomOBCControl()
    {
        Span<byte> data = stackalloc byte[14];
        Random r = Random.Shared;

        // Byte 0-4: BitArray 编码的控制位（模拟 OBCControl.ToBytesArray() 结构）
        data[0] = (byte)r.Next(0, 256);
        data[1] = (byte)r.Next(0, 256);
        data[2] = (byte)r.Next(0, 256);
        data[3] = (byte)r.Next(0, 256);
        data[4] = (byte)r.Next(0, 256);
        // Byte 5-12: 保留
        for (int i = 5; i < 13; i++)
            data[i] = 0;
        // Byte 13: BrakeLevel
        data[13] = (byte)r.Next(0, 8);

        return data.ToArray();
    }
}
