using System.Reflection.Emit;

namespace MOCS.Cores.MCU
{
    public class MCUStatus
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
        /// 牵引状态报文发送理由
        /// </summary>
        public MCUSendReasonEnum MCUSendReason { get; set; } = MCUSendReasonEnum.Response;

        /// <summary>
        /// 需要MOCS重复发送以前报文的数量
        /// </summary>
        public byte MsgNumToRepeat { get; set; } = 0;

        /// <summary>
        /// A类序列号参考点
        /// </summary>
        public ushort A_SequenceNumRefPoint { get; set; } = 0;

        /// <summary>
        /// 牵引状态改变就绪信息
        /// </summary>
        public MCUStatusChangeReadinessInfoEnum MCUStatusChangeReadinessInfo { get; set; } =
            MCUStatusChangeReadinessInfoEnum.None;

        /// <summary>
        /// “当前”悬浮架的悬浮架标识号
        /// 0表示没有悬浮架
        /// </summary>
        public byte CurrentMaglevVehicleIdentifier { get; set; } = 0;

        /// <summary>
        /// 清除“当前”悬浮架就绪信息（由于分区切换）
        /// </summary>
        public ClearMaglevVehicleReadinessInfoEnum ClearCurrentMaglevVehicleReadinessInfo { get; set; } =
            ClearMaglevVehicleReadinessInfoEnum.None;

        /// <summary>
        /// DCS编号: 如果悬浮架根据线路数据位于本分区内则为本分区编号，
        /// 否则为沿线路数据方向的上一个分区编号，用于分区交接。
        /// </summary>
        public byte DCSNum { get; set; } = 0;

        /// <summary>
        /// “当前”悬浮架的当前位置，相对于悬浮架中心
        /// 单位: cm
        /// </summary>
        public int CurrentMaglevVehiclePos { get; set; } = 0;

        /// <summary>
        /// “当前”悬浮架的实际速度
        /// 单位: cm/s
        /// </summary>
        public short CurrentMaglevVehicleVelocity { get; set; } = 0;

        /// <summary>
        /// “当前”悬浮架的实际加速度
        /// 单位: cm/s^2
        /// </summary>
        public short CurrentMaglevVehicleAcc { get; set; } = 0;

        /// <summary>
        /// 牵引系统对“当前”悬浮架牵引能力的百分比
        /// </summary>
        public byte TractionCapacityForCurrentVehicle { get; set; } = 0;

        /// <summary>
        /// “当前”悬浮架停车点运行状态
        /// </summary>
        public CurrentMaglevVehicleSPRStatusEnum CurrentMaglevVehicleSPRStatus { get; set; } =
            CurrentMaglevVehicleSPRStatusEnum.UnDefine;

        /// <summary>
        /// “虚拟”悬浮架的悬浮架标识号
        /// </summary>
        public byte VirtualMaglevVehicleIdentifier { get; set; } = 0;

        /// <summary>
        /// 清除“虚拟”悬浮架就绪信息（由于分区切换）
        /// </summary>
        public ClearMaglevVehicleReadinessInfoEnum ClearVirtualMaglevVehicleReadinessInfo { get; set; } =
            ClearMaglevVehicleReadinessInfoEnum.None;

        /// <summary>
        /// 牵引系统对“虚拟”悬浮架牵引能力的百分比
        /// </summary>
        public byte TractionCapacityForVirtualVehicle { get; set; } = 0;

        /// <summary>
        /// 牵引故障状态
        /// </summary>
        public MCUFaultStatusEnum MCUFaultStatus { get; set; } = MCUFaultStatusEnum.UnDefine;

        /// <summary>
        /// 牵引系统已经管理的停车点运行数量
        /// </summary>
        public byte ParkingPointsNum { get; set; } = 0;

        /// <summary>
        /// 对“当前”悬浮架的5个停车点运行的调试信息
        /// </summary>
        public byte[] CurrentKIIdentifiers { get; set; } = new byte[5];

        /// <summary>
        /// 对“虚拟”悬浮架的5个停车点运行的调试信息
        /// </summary>
        public byte[] VirtualKIIdentifiers { get; set; } = new byte[5];

        public void Reset()
        {
            Channel1RecvStatus = ChannelRecvStatusEnum.Normal;

            Channel2RecvStatus = ChannelRecvStatusEnum.Normal;

            MCUSendReason = MCUSendReasonEnum.Response;

            MsgNumToRepeat = 0;

            A_SequenceNumRefPoint = 0;

            MCUStatusChangeReadinessInfo = MCUStatusChangeReadinessInfoEnum.None;

            CurrentMaglevVehicleIdentifier = 0;

            ClearCurrentMaglevVehicleReadinessInfo = ClearMaglevVehicleReadinessInfoEnum.None;

            DCSNum = 0;

            CurrentMaglevVehiclePos = 0;

            CurrentMaglevVehicleVelocity = 0;

            CurrentMaglevVehicleAcc = 0;

            TractionCapacityForCurrentVehicle = 0;

            CurrentMaglevVehicleSPRStatus = CurrentMaglevVehicleSPRStatusEnum.UnDefine;

            VirtualMaglevVehicleIdentifier = 0;

            ClearVirtualMaglevVehicleReadinessInfo = ClearMaglevVehicleReadinessInfoEnum.None;

            TractionCapacityForVirtualVehicle = 0;

            MCUFaultStatus = MCUFaultStatusEnum.UnDefine;

            ParkingPointsNum = 0;
        }
    }
}
