using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MOCS.Cores.MCU
{
    public class MCUStatus : INotifyPropertyChanged
    {
        #region 单例模式
        private static readonly MCUStatus _instance = new MCUStatus();
        public static MCUStatus Instance => _instance;

        /// <summary>
        /// 私有构造函数，防止外部 new
        /// </summary>
        private MCUStatus() { }
        #endregion

        #region 接口实现（属性变更通知核心）
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region 通讯状态
        private ChannelRecvStatusEnum _channel1RecvStatus = ChannelRecvStatusEnum.Normal;
        private ChannelRecvStatusEnum _channel2RecvStatus = ChannelRecvStatusEnum.Normal;
        private MCUSendReasonEnum _mcuSendReason = MCUSendReasonEnum.Response;

        /// <summary>
        /// 接收通道1状态
        /// </summary>
        public ChannelRecvStatusEnum Channel1RecvStatus
        {
            get => _channel1RecvStatus;
            set { _channel1RecvStatus = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 接收通道2状态
        /// </summary>
        public ChannelRecvStatusEnum Channel2RecvStatus
        {
            get => _channel2RecvStatus;
            set { _channel2RecvStatus = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 牵引状态报文发送理由
        /// </summary>
        public MCUSendReasonEnum MCUSendReason
        {
            get => _mcuSendReason;
            set { _mcuSendReason = value; OnPropertyChanged(); }
        }
        #endregion

        #region 序列号与就绪
        private byte _msgNumToRepeat = 0;
        private ushort _aSequenceNumRefPoint = 0;
        private MCUStatusChangeReadinessInfoEnum _mcuStatusChangeReadinessInfo = MCUStatusChangeReadinessInfoEnum.None;

        /// <summary>
        /// 需要MOCS重复发送以前报文的数量
        /// </summary>
        public byte MsgNumToRepeat
        {
            get => _msgNumToRepeat;
            set { _msgNumToRepeat = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// A类序列号参考点
        /// </summary>
        public ushort A_SequenceNumRefPoint
        {
            get => _aSequenceNumRefPoint;
            set { _aSequenceNumRefPoint = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 牵引状态改变就绪信息
        /// </summary>
        public MCUStatusChangeReadinessInfoEnum MCUStatusChangeReadinessInfo
        {
            get => _mcuStatusChangeReadinessInfo;
            set { _mcuStatusChangeReadinessInfo = value; OnPropertyChanged(); }
        }
        #endregion

        #region 当前悬浮架信息
        private byte _currentMaglevVehicleIdentifier = 0;
        private ClearMaglevVehicleReadinessInfoEnum _clearCurrentMaglevVehicleReadinessInfo = ClearMaglevVehicleReadinessInfoEnum.None;
        private byte _dcsNum = 0;
        private int _currentMaglevVehiclePos = 0;
        private short _currentMaglevVehicleVelocity = 0;
        private short _currentMaglevVehicleAcc = 0;
        private byte _tractionCapacityForCurrentVehicle = 0;
        private CurrentMaglevVehicleSPRStatusEnum _currentMaglevVehicleSPRStatus = CurrentMaglevVehicleSPRStatusEnum.UnDefine;

        /// <summary>
        /// "当前"悬浮架的悬浮架标识号
        /// </summary>
        public byte CurrentMaglevVehicleIdentifier
        {
            get => _currentMaglevVehicleIdentifier;
            set { _currentMaglevVehicleIdentifier = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 清除"当前"悬浮架就绪信息（由于分区切换）
        /// </summary>
        public ClearMaglevVehicleReadinessInfoEnum ClearCurrentMaglevVehicleReadinessInfo
        {
            get => _clearCurrentMaglevVehicleReadinessInfo;
            set { _clearCurrentMaglevVehicleReadinessInfo = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// DCS编号
        /// </summary>
        public byte DCSNum
        {
            get => _dcsNum;
            set { _dcsNum = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// "当前"悬浮架的当前位置，相对于悬浮架中心 (cm)
        /// </summary>
        public int CurrentMaglevVehiclePos
        {
            get => _currentMaglevVehiclePos;
            set { _currentMaglevVehiclePos = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// "当前"悬浮架的实际速度 (cm/s)
        /// </summary>
        public short CurrentMaglevVehicleVelocity
        {
            get => _currentMaglevVehicleVelocity;
            set { _currentMaglevVehicleVelocity = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// "当前"悬浮架的实际加速度 (cm/s^2)
        /// </summary>
        public short CurrentMaglevVehicleAcc
        {
            get => _currentMaglevVehicleAcc;
            set { _currentMaglevVehicleAcc = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 牵引系统对"当前"悬浮架牵引能力的百分比
        /// </summary>
        public byte TractionCapacityForCurrentVehicle
        {
            get => _tractionCapacityForCurrentVehicle;
            set { _tractionCapacityForCurrentVehicle = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// "当前"悬浮架停车点运行状态
        /// </summary>
        public CurrentMaglevVehicleSPRStatusEnum CurrentMaglevVehicleSPRStatus
        {
            get => _currentMaglevVehicleSPRStatus;
            set { _currentMaglevVehicleSPRStatus = value; OnPropertyChanged(); }
        }
        #endregion

        #region 虚拟悬浮架信息
        private byte _virtualMaglevVehicleIdentifier = 0;
        private ClearMaglevVehicleReadinessInfoEnum _clearVirtualMaglevVehicleReadinessInfo = ClearMaglevVehicleReadinessInfoEnum.None;
        private byte _tractionCapacityForVirtualVehicle = 0;

        /// <summary>
        /// "虚拟"悬浮架的悬浮架标识号
        /// </summary>
        public byte VirtualMaglevVehicleIdentifier
        {
            get => _virtualMaglevVehicleIdentifier;
            set { _virtualMaglevVehicleIdentifier = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 清除"虚拟"悬浮架就绪信息（由于分区切换）
        /// </summary>
        public ClearMaglevVehicleReadinessInfoEnum ClearVirtualMaglevVehicleReadinessInfo
        {
            get => _clearVirtualMaglevVehicleReadinessInfo;
            set { _clearVirtualMaglevVehicleReadinessInfo = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 牵引系统对"虚拟"悬浮架牵引能力的百分比
        /// </summary>
        public byte TractionCapacityForVirtualVehicle
        {
            get => _tractionCapacityForVirtualVehicle;
            set { _tractionCapacityForVirtualVehicle = value; OnPropertyChanged(); }
        }
        #endregion

        #region 故障与停车点
        private MCUFaultStatusEnum _mcuFaultStatus = MCUFaultStatusEnum.UnDefine;
        private byte _parkingPointsNum = 0;
        private byte[] _currentKIIdentifiers = new byte[5];
        private byte[] _virtualKIIdentifiers = new byte[5];

        /// <summary>
        /// 牵引故障状态
        /// </summary>
        public MCUFaultStatusEnum MCUFaultStatus
        {
            get => _mcuFaultStatus;
            set { _mcuFaultStatus = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 牵引系统已经管理的停车点运行数量
        /// </summary>
        public byte ParkingPointsNum
        {
            get => _parkingPointsNum;
            set { _parkingPointsNum = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 对"当前"悬浮架的5个停车点运行的调试信息
        /// </summary>
        public byte[] CurrentKIIdentifiers
        {
            get => _currentKIIdentifiers;
            set { _currentKIIdentifiers = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 对"虚拟"悬浮架的5个停车点运行的调试信息
        /// </summary>
        public byte[] VirtualKIIdentifiers
        {
            get => _virtualKIIdentifiers;
            set { _virtualKIIdentifiers = value; OnPropertyChanged(); }
        }
        #endregion

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
            CurrentKIIdentifiers = new byte[5];
            VirtualKIIdentifiers = new byte[5];
        }
    }
}
