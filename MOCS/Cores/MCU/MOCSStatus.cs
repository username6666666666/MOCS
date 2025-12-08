using System.Buffers.Binary;

namespace MOCS.Cores.MCU
{
    public class MOCSStatus
    {
        /// <summary>
        /// 接收通道1状态
        /// </summary>
        public ChannelRecvStatusEnum Channel1RecvStatus { get; set; } =
            ChannelRecvStatusEnum.Normal;

        /// <summary>
        /// 接收通道2状态
        /// </summary>
        public ChannelRecvStatusEnum Channel2RecvStatus { get; set; } =
            ChannelRecvStatusEnum.Normal;

        /// <summary>
        /// 牵引系统状态报文接收状态
        /// </summary>
        public MCUStatusMessageRecvStatusEnum MCUStatusMessageRecvStatus { get; set; } =
            MCUStatusMessageRecvStatusEnum.NoRecv;

        /// <summary>
        /// 与牵引状态改变就绪信息有关的请求
        /// </summary>
        public RequestForMCUStatusChangeReadinessEnum RequestForMCUStatusChangeReady { get; set; } =
            RequestForMCUStatusChangeReadinessEnum.None;

        /// <summary>
        /// 上次应答的报文序列号（A类）
        /// </summary>
        public ushort LastResponseMsgId { get; set; } = 0;

        /// <summary>
        /// MOCS可重复报文的最大数量
        /// </summary>
        public byte MaxNumForRepeat { get; set; } = 0;

        /// <summary>
        /// “当前”悬浮架标识号
        /// </summary>
        public byte CurrentMaglevVehicleIdentifier { get; set; } = 0;

        /// <summary>
        /// 悬浮/落下命令处理状态
        /// </summary>
        public MaglevVehicleLeviCommandStatusEnum MaglevVehicleLeviCommandStatus { get; set; } =
            MaglevVehicleLeviCommandStatusEnum.UnDefined;

        /// <summary>
        /// 悬浮架悬浮/落下状态
        /// </summary>
        public MaglevVehicleLeviStatusEnum MaglevVehicleLeviStatus { get; set; } =
            MaglevVehicleLeviStatusEnum.UnDefined;

        /// <summary>
        /// 期望速度的默认类型
        /// </summary>
        public ExpectedSpeedTypeEnum ExpectedSpeedType { get; set; } = ExpectedSpeedTypeEnum.Direct;

        /// <summary>
        /// 期望运行方向
        /// </summary>
        public ExpectedRunningDircetionEnum ExpectedRunningDircetion { get; set; } =
            ExpectedRunningDircetionEnum.Same;

        /// <summary>
        /// 期望速度大小或百分比
        /// </summary>
        public short ExpectedSpeed { get; set; } = 0;

        /// <summary>
        /// 加速度限制，牵引系统加速或制动时不能超过该限制
        /// </summary>
        public short MaxAcc { get; set; } = 0;

        /// <summary>
        /// 最小速度
        /// </summary>
        public short MinSpeed { get; set; } = 0;

        /// <summary>
        /// 诊断信息
        /// </summary>
        public DiagnosticInfoEnum DiagnosticInfo { get; set; } = DiagnosticInfoEnum.None;

        public void Reset()
        {
            Channel1RecvStatus = ChannelRecvStatusEnum.Normal;

            Channel2RecvStatus = ChannelRecvStatusEnum.Normal;

            MCUStatusMessageRecvStatus = MCUStatusMessageRecvStatusEnum.NoRecv;

            RequestForMCUStatusChangeReady = RequestForMCUStatusChangeReadinessEnum.None;

            LastResponseMsgId = 0;

            MaxNumForRepeat = 0;

            CurrentMaglevVehicleIdentifier = 0;

            MaglevVehicleLeviCommandStatus = MaglevVehicleLeviCommandStatusEnum.UnDefined;

            MaglevVehicleLeviStatus = MaglevVehicleLeviStatusEnum.UnDefined;

            ExpectedSpeedType = ExpectedSpeedTypeEnum.Direct;

            ExpectedRunningDircetion = ExpectedRunningDircetionEnum.Same;

            ExpectedSpeed = 0;

            MaxAcc = 0;

            MinSpeed = 0;

            DiagnosticInfo = DiagnosticInfoEnum.None;
        }

        public byte[] ToByteArray()
        {
            Span<byte> data = stackalloc byte[148];

            data[0] = (byte)Channel1RecvStatus;
            data[2] = (byte)Channel2RecvStatus;
            data[4] = (byte)MCUStatusMessageRecvStatus;
            data[5] = (byte)RequestForMCUStatusChangeReady;
            BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(6, 2), LastResponseMsgId);
            data[8] = MaxNumForRepeat;
            data[9] = CurrentMaglevVehicleIdentifier;
            data[10] = (byte)((byte)MaglevVehicleLeviCommandStatus | (byte)MaglevVehicleLeviStatus);
            data[11] = (byte)((byte)ExpectedSpeedType | (byte)ExpectedRunningDircetion);
            BinaryPrimitives.WriteInt16LittleEndian(data.Slice(12, 2), ExpectedSpeed);
            BinaryPrimitives.WriteInt16LittleEndian(data.Slice(14, 2), MaxAcc);
            BinaryPrimitives.WriteInt16LittleEndian(data.Slice(16, 2), MinSpeed);
            data[18] = (byte)DiagnosticInfo;

            return data.ToArray();
        }
    }
}
