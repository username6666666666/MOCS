namespace MOCS.Cores.MCU
{
    #region 牵引系统通讯接口状态机
    public enum MCUInterfaceState
    {
        Stop, // 停止状态
        UnConnected, // 未连接状态
        Connected, // 连接状态

        // 连接状态下的牵引系统子状态
        UnKnown, // 未知状态，Connected状态会自动进入此状态

        Initial, // 初始状态
        Basic, // 基本状态
        HotStandby, // 带电等待状态
        MaglevFrameRunning, // 悬浮架运行状态
        ReadyForTest, // 准备测试状态
        HotTractionTest, // 带电牵引测试状态
        DeadTractionTest, // 不带电牵引测试状态
        SimulatedRunning, // 模拟运行状态
    }

    public enum MCUInterfaceTrigger
    {
        Activate, // 启动
        Deactivate, // 关闭
        MOCSLifeCycleMsgSend, // MOCS生命周期（状态）报文发送
        MCULifeCycleMsgRecvTimeOut, // MCU生命周期（状态）报文接收超时
        MCULifeCycleMsgRecvd, // 收到MCU生命周期（状态）报文
        MCUAckRecvTimeOut, // 超时未收到MCU应答报文
        MCUAckRecvd, // 接收到应答报文
        ChangeMCUState, // 改变MCU状态
        MCUStateHasChanged, // 牵引系统状态改变完成

        // 以下触发器只在状态为“悬浮架运行”时有效
        MaglevVehicleLoginIn, // 悬浮架登录

        MaglevVehicleSignOut, // 悬浮架注销
        TransmitTrackData, // 传输线路数据
        DeleteTrackData, // 删除线路数据
        TransmitMaximumCurve, // 传输最大速度曲线
        DeleteMaximumCurve, // 删除最大速度曲线
        CalBrakeCurve, // 计算制动曲线
    }

    public enum MCUStateChangeCommand
    {
        // 从初始状态转换
        PBS, // 基本状态

        // 从基本状态转换
        PSIM, // 模拟运行状态

        PT, // 不带电牵引测试状态
        PTP, // 带电牵引测试状态
        PWSP, // 带电等待状态

        // 从带电等待状态转换
        PTO, // 悬浮架运行状态

        PPT, // 准备测试状态
    }

    public enum MCUStateChangeMonitorState
    {
        Stop,
        Unchanged,
        IsChanging,
        Changed,
    }

    public enum MCUStateChangeMonitorTrigger
    {
        Activate,
        Deactivate,
        Executed,
        UnExecute,
    }
    #endregion

    /// <summary>
    /// 通道接收状态
    /// Normal - 接收通道正常
    /// CanNotRecv - 此通道接收不到报文
    /// OnlyRecvFaultMsg - 通道只能接收到错误报文
    /// CRCErrorOnce - 发生一次CRC错误
    /// OnlyMsgIdError - 只有报文标识号错误, CRC正确
    /// OnlyDesIdError  - 只有目的方编码错误
    /// OnlySrcIdError - 只有发送方编码错误
    /// </summary>
    /// <remarks>注意通道编号</remarks>
    public enum ChannelRecvStatusEnum : byte
    {
        Normal = 0x00,
        CanNotRecv = 1 << 0,
        OnlyRecvFaultMsg = 1 << 1,
        CRCErrorOnce = 1 << 2,
        OnlyMsgIdError = 1 << 3,
        OnlyDesIdError = 1 << 4,
        OnlySrcIdError = 1 << 5,
    }

    #region MOCS状态报文

    /// <summary>
    /// 牵引系统状态报文接收状态
    /// HasRecvd - 已收到牵引控制系统对上一次MOCS状态报文响应的报文
    /// NoRecv - 没有收到牵引控制系统对上一次MOCS状态报文响应的报文
    /// </summary>
    public enum MCUStatusMessageRecvStatusEnum : byte
    {
        HasRecvd = 0x00,
        NoRecv = 1,
    }

    /// <summary>
    /// 与牵引状态改变就绪有关的请求
    /// None - 无与就绪信息有关的请求
    /// Delete - 删除牵引状态改变就绪信息
    /// </summary>
    public enum RequestForMCUStatusChangeReadinessEnum : byte
    {
        None = 0x01,
        Delete = 0x02,
    }

    /// <summary>
    /// 悬浮架状态
    /// 悬浮架悬浮/落下命令处理状态
    /// UnDefined - 未定义/MOCS未发出命令
    /// Leviate - MOCS发出悬浮架悬浮命令并正在处理中
    /// Land - MOCS发出悬浮架落下命令并正在处理中
    /// </summary>
    public enum MaglevVehicleLeviCommandStatusEnum : byte
    {
        UnDefined = 0x00,
        Leviate = 1 << 0,
        Land = 1 << 1,
    }

    /// <summary>
    /// 悬浮架状态
    /// 悬浮架悬浮/落下状态
    /// UnDefined - 未定义
    /// Land - 悬浮架安全的处于standstill（落下）
    /// Leviating - 悬浮架未悬浮
    /// Leviated - 悬浮架已悬浮
    /// </summary>
    public enum MaglevVehicleLeviStatusEnum : byte
    {
        UnDefined = 0x00,
        Land = 1 << 2,
        Leviating = 1 << 3,
        Leviated = 0b_0000_1100,
    }

    /// <summary>
    /// 运行规格
    /// 期望速度的默认类型
    /// Direct - 直接
    /// Percent - 百分比
    /// </summary>
    public enum ExpectedSpeedTypeEnum : byte
    {
        Direct = 0x00,
        Percent = 0b_0000_0011,
    }

    /// <summary>
    /// 运行规格
    /// 期望运行方向
    /// Same - 与线路数据方向相同
    /// Opposite - 与线路数据方向相反
    /// </summary>
    public enum ExpectedRunningDircetionEnum : byte
    {
        Same = 0x00,
        Opposite = 0b_0011_0000,
    }

    /// <summary>
    /// 系统诊断信息
    /// None - 无信息
    /// Error - 发生一个错误
    /// CanNotHandleResendRequest - 不能处理牵引系统的报文重发请求
    /// </summary>
    public enum DiagnosticInfoEnum : byte
    {
        None = 0x00,
        Error = 1 << 0,
        CanNotHandleResendRequest = 1 << 1,
    }
    #endregion

    #region 悬浮架登录报文

    /// <summary>
    /// 悬浮架类型
    /// MaglevFrame - 磁浮悬浮架
    /// </summary>
    /// <remarks>第1位~第7位有预留</remarks>
    public enum MaglevVehicleTypeEnum : byte
    {
        MaglevFrame = 0,
    }

    /// <summary>
    /// 悬浮架方向
    /// Unknown - 悬浮架方向未知；悬浮架不在本分区，
    /// 或者沿线路数据方向不在上一个分区的边界范围内
    /// Increase - 悬浮架头车1 的方向为轨道静态公里标递增的方向
    /// Decrease - 悬浮架头车1 的方向为轨道静态公里标递减的方向
    /// </summary>
    /// <remarks>第2位~第7位有预留</remarks>
    public enum MaglevVehicleDirectionEnum : byte
    {
        Unknown = 0x00,
        Increase = 1 << 0,
        Decrease = 1 << 1,
    }

    #endregion

    #region 最大速度曲线报文

    /// <summary>
    /// 本分区内发往MCU的最后一个最大速度曲线报文标志位
    /// </summary>
    public enum IsLastMsgFlagEnum : byte
    {
        IsLast = 0x00,
        NotLast = 0x01,
    }

    #endregion

    #region 传输线路数据报文

    /// <summary>
    /// 停车点类型
    /// Station - 车站
    /// ParkingPoint - 停车点
    /// ServiceArea - 服务区
    /// </summary>
    public enum ParkingPointTypeEnum : byte
    {
        Station = 1 << 0,
        ParkingPoint = 1 << 1,
        ServiceArea = 1 << 2,
    }

    #endregion

    #region 删除线路数据报文

    /// <summary>
    /// 删除范围标志
    /// Specified - 删除指定KI的停车点运行数据
    /// AllFromSpecified - 沿线路数据方向从指定位置删除所有线路数据
    /// All - 删除所有线路数据
    /// </summary>
    public enum RemoveFlagEnum : byte
    {
        Specified = 1 << 0,
        AllFromSpecified = 1 << 1,
        All = 1 << 2,
    }

    #endregion

    #region 改变牵引状态报文

    /// <summary>
    /// 要求切换到的状态
    /// GetCurrent - 牵引系统提交当前状态
    /// Initial - 初始状态
    /// Basic - 基本状态
    /// HotStandby - 带电等待状态
    /// MaglevVehicleRunning - 悬浮架运行
    /// ReadyForTest - 准备测试
    /// HotTractionTest - 带电牵引测试
    /// DeadTractionTest - 不带电牵引测试
    /// SimulatedRunning - 模拟运行
    /// </summary>
    public enum TargetStateEnum : byte
    {
        GetCurrent = 0x00,
        Initial = 0x01,
        Basic = 0x02,
        HotStandby = 0x04,
        MaglevVehicleRunning = 0x08,
        ReadyForTest = 0x10,
        HotTractionTest = 0x20,
        DeadTractionTest = 0x40,
        SimulatedRunning = 0x80,
    }

    #endregion

    #region MCU状态与应答报文

    /// <summary>
    /// 牵引状态报文发送理由（响应MOCS）
    /// Response - 本报文为响应MOCS状态报文而发送
    /// TimeOut - 本报文因为“牵引状态报文”时间超时而发送
    /// </summary>
    public enum MCUSendReasonEnum : byte
    {
        Response = 0,
        TimeOut = 1,
    }

    /// <summary>
    /// 牵引状态改变就绪信息
    /// None - 没有牵引状态改变就绪
    /// AlReady_Activate - 牵引状态改变就绪 （MCU启动）
    /// AlReady_Normal - 牵引状态改变就绪 （正常操作）
    /// </summary>
    public enum MCUStatusChangeReadinessInfoEnum : byte
    {
        None = 1 << 0,
        AlReady_Activate = 1 << 1,
        AlReady_Normal = 1 << 2,
    }

    /// <summary>
    /// 清除悬浮架就绪信息（由于分区切换）
    /// None - 无就绪信息
    /// Ready - 注销就绪
    /// </summary>
    /// <remarks>注意区分“当前”和“虚拟”</remarks>
    public enum ClearMaglevVehicleReadinessInfoEnum : byte
    {
        None = 1 << 0,
        Ready = 1 << 1,
    }

    /// <summary>
    /// “当前”悬浮架停车点运行状态
    /// UnDefine - 未定义（例如，当前停车点未知，由于丢失位置信息导致无法停车点运行等）
    /// UnComplete - 当前停车点运行未完成
    /// Acurate - 当前停车点运行已完成，悬浮架精确停车到当前停车点的目标点并处于standstill状态
    /// UnAcurate - 当前停车点运行已完成，但悬浮架没有精确停车到当前停车点的目标点并处于standstill状态
    /// </summary>
    public enum CurrentMaglevVehicleSPRStatusEnum : byte
    {
        UnDefine = 1 << 0,
        UnComplete = 1 << 1,
        Acurate = 1 << 2,
        UnAcurate = 1 << 3,
    }

    /// <summary>
    /// 牵引故障状态状态
    /// Fault0 - 故障0
    /// Fault1 - 故障1
    /// Fault2 - 故障2
    /// Fault3 - 故障3
    /// UnDefine - 未定义
    /// </summary>
    public enum MCUFaultStatusEnum : byte
    {
        Fault0 = 0x01,
        Fault1 = 0x02,
        Fault2 = 0x04,
        Fault3 = 0x08,
        UnDefine = 0xFF,
    }

    /// <summary>
    /// 牵引应答报文错误标识号
    /// NoError - 没有错误
    /// NoError_AlreadyInExpectedState - 没有错误请求时
    /// 已处于期望的状态（例如，删除未知悬浮架编号的悬浮架数据，
    /// 删除未知KI的线路数据）
    /// SequenceNumberInvalid - 序列号错误
    /// </summary>
    public enum MCUReplyErrorIdentifierEnum : byte
    {
        NoError = 0,
        NoError_AlreadyInExpectedState = 1,
        SequenceNumberInvalid = 2,
        ///其他错误标识号待定
    }

    /// <summary>
    /// 处理状态（牵引状态改变，曲线计算）
    /// None - 没有处理状态
    /// Processing - 请求正在处理
    /// Processed - 请求已执行
    /// ErrorOccurred - 处理过程中发生错误
    /// </summary>
    public enum MCUProcessStatusEnum : byte
    {
        None = 0,
        Processing = 0x01,
        Processed = 0x02,
        ErrorOccurred = 0x03,
    }

    /// <summary>
    /// 牵引应答报文请求当前管理的牵引状态
    /// Initial - 初始状态
    /// Basic - 基本状态
    /// HotStandby - 带电等待状态
    /// MaglevVehicleRunning - 悬浮架运行
    /// ReadyForTest - 准备测试
    /// HotTractionTest - 带电牵引测试
    /// DeadTractionTest - 不带电牵引测试
    /// SimulatedRunning - 模拟运行
    /// </summary>
    public enum MCUCurrentTractionStateEnum : byte
    {
        Initial = 0x01,
        Basic = 0x02,
        HotStandby = 0x04,
        MaglevVehicleRunning = 0x08,
        ReadyForTest = 0x10,
        HotTractionTest = 0x20,
        DeadTractionTest = 0x40,
        SimulatedRunning = 0x80,
    }

    #endregion
}
