using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace MOCS.Cores.VCU
{
    /// <summary>
    /// 悬浮控制器状态
    /// </summary>
    public class EMSStatus:INotifyPropertyChanged
    {
        #region 单例模式
        private static readonly EMSStatus _instance = new EMSStatus();
        public static EMSStatus Instance => _instance;
        
        /// <summary>
        /// 私有构造函数，防止外部 new
        /// </summary>
        private EMSStatus() { }
        #endregion

        #region 私有字段（与公共属性一一对应）
        private GapSensorsStatusEnum _gapSensorsStatus = GapSensorsStatusEnum.Normal;
        private GapSensorStatusEnum _gapSensor1Status = GapSensorStatusEnum.Normal;
        private GapSensorStatusEnum _gapSensor2Status = GapSensorStatusEnum.Normal;
        private GapSensorStatusEnum _gapSensor3Status = GapSensorStatusEnum.Normal;
        private GapSensorStatusEnum _gapSensor4Status = GapSensorStatusEnum.Normal;
        private EMSCmdStatusEnum _emsCmd = EMSCmdStatusEnum.DeEnergize;
        private EMSSysStatusEnum _emsSysStatus = EMSSysStatusEnum.Normal;
        private OverloadStatusEnum _overloadStatus = OverloadStatusEnum.Deactivate;
        private AccSensorsStatusEnum _accSensorsStatus = AccSensorsStatusEnum.Normal;
        private AccSensorStatusEnum _accSensor1Status = AccSensorStatusEnum.Normal;
        private AccSensorStatusEnum _accSensor2Status = AccSensorStatusEnum.Normal;
        private EMSOperationStatusEnum _emsOperationStatus = EMSOperationStatusEnum.DeEnergized;
        private EMSWarningEnum _emsWarning = EMSWarningEnum.Normal;
        private EMSFaultStatusEnum _emsFaultStatus = EMSFaultStatusEnum.Normal;
        private KMStatusEnum _km1Status = KMStatusEnum.Normal;
        private KMStatusEnum _km2Status = KMStatusEnum.Normal;
        private KMContactStatusEnum _km1ContactStatus = KMContactStatusEnum.Open;
        private KMContactStatusEnum _km2ContactStatus = KMContactStatusEnum.Open;
        private CPUStatusEnum _cpuStatus = CPUStatusEnum.Normal;
        private CutStatusEnum _cutStatus = CutStatusEnum.Normal;
        private StabilityStatusEnum _stabilityStatus = StabilityStatusEnum.Normal;
        private OverloadWarningStatusEnum _overloadWarningStatus = OverloadWarningStatusEnum.Normal;
        private GapWarnningStatusEnum _gapWarnningStatus = GapWarnningStatusEnum.Normal;
        private SysSwitchStatusEnum _sysSwitchStatus = SysSwitchStatusEnum.NoChange;
        private BrakeStatusEnum _brakeStatus = BrakeStatusEnum.NoBrake;
        private byte _life = 0;
        private int _temp = 20;
        private float _gap = 0;
        private float _gap1 = 0;
        private float _gap2 = 0;
        private float _gap3 = 0;
        private float _gap4 = 0;
        private float _u = 0;
        private float _i1 = 0;
        private float _i2 = 0;
        private float _acc = 0;
        private float _acc1 = 0;
        private float _acc2 = 0;
        #endregion

        #region 公共属性（带变更通知）
        public GapSensorsStatusEnum GapSensorsStatus
        {
            get => _gapSensorsStatus;
            set { SetField(ref _gapSensorsStatus, value); }
        }
        public GapSensorStatusEnum GapSensor1Status
        {
            get => _gapSensor1Status;
            set { SetField(ref _gapSensor1Status, value); }
        }
        public GapSensorStatusEnum GapSensor2Status
        {
            get => _gapSensor2Status;
            set { SetField(ref _gapSensor2Status, value); }
        }
        public GapSensorStatusEnum GapSensor3Status
        {
            get => _gapSensor3Status;
            set { SetField(ref _gapSensor3Status, value); }
        }
        public GapSensorStatusEnum GapSensor4Status
        {
            get => _gapSensor4Status;
            set { SetField(ref _gapSensor4Status, value); }
        }
        public EMSCmdStatusEnum EMSCmd
        {
            get => _emsCmd;
            set { SetField(ref _emsCmd, value); }
        }
        public EMSSysStatusEnum EMSSysStatus
        {
            get => _emsSysStatus;
            set { SetField(ref _emsSysStatus, value); }
        }
        public OverloadStatusEnum OverloadStatus
        {
            get => _overloadStatus;
            set { SetField(ref _overloadStatus, value); }
        }
        public AccSensorsStatusEnum AccSensorsStatus
        {
            get => _accSensorsStatus;
            set { SetField(ref _accSensorsStatus, value); }
        }
        public AccSensorStatusEnum AccSensor1Status
        {
            get => _accSensor1Status;
            set { SetField(ref _accSensor1Status, value); }
        }
        public AccSensorStatusEnum AccSensor2Status
        {
            get => _accSensor2Status;
            set { SetField(ref _accSensor2Status, value); }
        }
        public EMSOperationStatusEnum EMSOperationStatus
        {
            get => _emsOperationStatus;
            set { SetField(ref _emsOperationStatus, value); }
        }
        public EMSWarningEnum EMSWarning
        {
            get => _emsWarning;
            set { SetField(ref _emsWarning, value); }
        }
        public EMSFaultStatusEnum EMSFaultStatus
        {
            get => _emsFaultStatus;
            set { SetField(ref _emsFaultStatus, value); }
        }
        public KMStatusEnum KM1Status
        {
            get => _km1Status;
            set { SetField(ref _km1Status, value); }
        }
        public KMStatusEnum KM2Status
        {
            get => _km2Status;
            set { SetField(ref _km2Status, value); }
        }
        public KMContactStatusEnum KM1ContactStatus
        {
            get => _km1ContactStatus;
            set { SetField(ref _km1ContactStatus, value); }
        }
        public KMContactStatusEnum KM2ContactStatus
        {
            get => _km2ContactStatus;
            set { SetField(ref _km2ContactStatus, value); }
        }
        public CPUStatusEnum CPUStatus
        {
            get => _cpuStatus;
            set { SetField(ref _cpuStatus, value); }
        }
        public CutStatusEnum CutStatus
        {
            get => _cutStatus;
            set { SetField(ref _cutStatus, value); }
        }
        public StabilityStatusEnum StabilityStatus
        {
            get => _stabilityStatus;
            set { SetField(ref _stabilityStatus, value); }
        }
        public OverloadWarningStatusEnum OverloadWarningStatus
        {
            get => _overloadWarningStatus;
            set { SetField(ref _overloadWarningStatus, value); }
        }
        public GapWarnningStatusEnum GapWarnningStatus
        {
            get => _gapWarnningStatus;
            set { SetField(ref _gapWarnningStatus, value); }
        }
        public SysSwitchStatusEnum SysSwitchStatus
        {
            get => _sysSwitchStatus;
            set { SetField(ref _sysSwitchStatus, value); }
        }
        public BrakeStatusEnum BrakeStatus
        {
            get => _brakeStatus;
            set { SetField(ref _brakeStatus, value); }
        }
        public byte Life
        {
            get => _life;
            set { SetField(ref _life, value); }
        }
        public int Temp
        {
            get => _temp;
            set { SetField(ref _temp, value); }
        }
        public float Gap
        {
            get => _gap;
            set { SetField(ref _gap, value); }
        }
        public float Gap1
        {
            get => _gap1;
            set { SetField(ref _gap1, value); }
        }
        public float Gap2
        {
            get => _gap2;
            set { SetField(ref _gap2, value); }
        }
        public float Gap3
        {
            get => _gap3;
            set { SetField(ref _gap3, value); }
        }
        public float Gap4
        {
            get => _gap4;
            set { SetField(ref _gap4, value); }
        }
        public float U
        {
            get => _u;
            set { SetField(ref _u, value); }
        }
        public float I1
        {
            get => _i1;
            set { SetField(ref _i1, value); }
        }
        public float I2
        {
            get => _i2;
            set { SetField(ref _i2, value); }
        }
        public float Acc
        {
            get => _acc;
            set { SetField(ref _acc, value); }
        }
        public float Acc1
        {
            get => _acc1;
            set { SetField(ref _acc1, value); }
        }
        public float Acc2
        {
            get => _acc2;
            set { SetField(ref _acc2, value); }
        }
        #endregion

        #region 接口实现（属性变更通知核心）
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 通用字段设置+通知方法（减少重复代码）
        /// </summary>
        /// <typeparam name="T">字段类型</typeparam>
        /// <param name="field">字段引用</param>
        /// <param name="value">新值</param>
        /// <param name="propertyName">属性名（自动获取）</param>
        protected virtual void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Reset()
        {
            GapSensorsStatus = GapSensorsStatusEnum.Normal;
            GapSensor1Status = GapSensorStatusEnum.Normal;
            GapSensor2Status = GapSensorStatusEnum.Normal;
            GapSensor3Status = GapSensorStatusEnum.Normal;
            GapSensor4Status = GapSensorStatusEnum.Normal;
            EMSCmd = EMSCmdStatusEnum.DeEnergize;
            EMSSysStatus = EMSSysStatusEnum.Normal;
            OverloadStatus = OverloadStatusEnum.Deactivate;
            AccSensorsStatus = AccSensorsStatusEnum.Normal;
            AccSensor1Status = AccSensorStatusEnum.Normal;
            AccSensor2Status = AccSensorStatusEnum.Normal;
            EMSOperationStatus = EMSOperationStatusEnum.DeEnergized;
            EMSWarning = EMSWarningEnum.Normal;
            EMSFaultStatus = EMSFaultStatusEnum.Normal;
            KM1Status = KMStatusEnum.Normal;
            KM2Status = KMStatusEnum.Normal;
            KM1ContactStatus = KMContactStatusEnum.Open;
            KM2ContactStatus = KMContactStatusEnum.Open;
            CPUStatus = CPUStatusEnum.Normal;
            CutStatus = CutStatusEnum.Normal;
            StabilityStatus = StabilityStatusEnum.Normal;
            OverloadWarningStatus = OverloadWarningStatusEnum.Normal;
            GapWarnningStatus = GapWarnningStatusEnum.Normal;
            SysSwitchStatus = SysSwitchStatusEnum.NoChange;
            BrakeStatus = BrakeStatusEnum.NoBrake;
            Life = 0;
            Temp = 0;
            Gap = 0;
            Gap1 = 0;
            Gap2 = 0;
            Gap3 = 0;
            Gap4 = 0;
            U = 0;
            I1 = 0;
            I2 = 0;
            Acc = 0;
            Acc1 = 0;
            Acc2 = 0;
        }
    }
}
