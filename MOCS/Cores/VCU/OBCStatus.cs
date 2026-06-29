using System;
using System.ComponentModel;

namespace MOCS.Cores.VCU
{
    public class OBCStatus: INotifyPropertyChanged
    {
        #region 单例模式
        private static readonly OBCStatus _instance = new OBCStatus();
        public static OBCStatus Instance => _instance;
        
        /// <summary>
        /// 私有构造函数，防止外部 new
        /// </summary>
        private OBCStatus() { }
        #endregion
        
        public event PropertyChangedEventHandler? PropertyChanged;

        //  触发事件的辅助方法
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // ========== 440V电源合闸状态属性 ==========
        private bool _is440VBatterySwitchClosed = false; // 私有字段（存储实际值）
        public bool Is440VBatterySwitchClosed
        {
            get => _is440VBatterySwitchClosed; // 获取值
            set
            {
                // 只有值变化时才触发更新（防闪烁）
                if (_is440VBatterySwitchClosed != value)
                {
                    _is440VBatterySwitchClosed = value;
                    // 触发属性变更事件（通知 UI 状态变了）
                    OnPropertyChanged(nameof(Is440VBatterySwitchClosed));
                }
            }
        }

        // ========== 480V输出合闸状态属性 ==========
        private bool _is480VPowerSwitchClosed = false;
        public bool Is480VPowerSwitchClosed
        {
            get => _is480VPowerSwitchClosed;
            set
            {
                if (_is480VPowerSwitchClosed != value)
                {
                    _is480VPowerSwitchClosed = value;
                    OnPropertyChanged(nameof(Is480VPowerSwitchClosed));
                }
            }
        }

        // DC330V断路器使能
        private bool _isDC330VCircuitBreakerEnabled = false;
        public bool IsDC330VCircuitBreakerEnabled
        {
            get => _isDC330VCircuitBreakerEnabled;
            set
            {
                if (_isDC330VCircuitBreakerEnabled != value)
                {
                    _isDC330VCircuitBreakerEnabled = value;
                    OnPropertyChanged(nameof(IsDC330VCircuitBreakerEnabled));
                }
            }
        }

        // 25kW电源故障
        private bool _is25kWPowerFailed = false;
        public bool Is25kWPowerFailed
        {
            get => _is25kWPowerFailed;
            set
            {
                if (_is25kWPowerFailed != value)
                {
                    _is25kWPowerFailed = value;
                    OnPropertyChanged(nameof(Is25kWPowerFailed));
                }
            }
        }

        // 5kW电源故障
        private bool _is5kWPowerFailed = false;
        public bool Is5kWPowerFailed
        {
            get => _is5kWPowerFailed;
            set
            {
                if (_is5kWPowerFailed != value)
                {
                    _is5kWPowerFailed = value;
                    OnPropertyChanged(nameof(Is5kWPowerFailed));
                }
            }
        }

        // 受流器带电
        private bool _isPantographEnergized = false;
        public bool IsPantographEnergized
        {
            get => _isPantographEnergized;
            set
            {
                if (_isPantographEnergized != value)
                {
                    _isPantographEnergized = value;
                    OnPropertyChanged(nameof(IsPantographEnergized));
                }
            }
        }

        // 受流器1升起
        private bool _isPantographExtended1 = false;
        public bool IsPantographExtended1
        {
            get => _isPantographExtended1;
            set
            {
                if (_isPantographExtended1 != value)
                {
                    _isPantographExtended1 = value;
                    OnPropertyChanged(nameof(IsPantographExtended1));
                }
            }
        }

        // 受流器2升起
        private bool _isPantographExtended2 = false;
        public bool IsPantographExtended2
        {
            get => _isPantographExtended2;
            set
            {
                if (_isPantographExtended2 != value)
                {
                    _isPantographExtended2 = value;
                    OnPropertyChanged(nameof(IsPantographExtended2));
                }
            }
        }

        // 受流器1收回
        private bool _isPantographRetracted1 = true;
        public bool IsPantographRetracted1
        {
            get => _isPantographRetracted1;
            set
            {
                if (_isPantographRetracted1 != value)
                {
                    _isPantographRetracted1 = value;
                    OnPropertyChanged(nameof(IsPantographRetracted1));
                }
            }
        }

        // 受流器2收回
        private bool _isPantographRetracted2 = true;
        public bool IsPantographRetracted2
        {
            get => _isPantographRetracted2;
            set
            {
                if (_isPantographRetracted2 != value)
                {
                    _isPantographRetracted2 = value;
                    OnPropertyChanged(nameof(IsPantographRetracted2));
                }
            }
        }

        // 悬浮状态
        private bool _isLeviated = false;
        public bool IsLeviated
        {
            get => _isLeviated;
            set
            {
                if (_isLeviated != value)
                {
                    _isLeviated = value;
                    OnPropertyChanged(nameof(IsLeviated));
                }
            }
        }

        // 导向使能
        private bool _isGuideEnabled = false;
        public bool IsGuideEnabled
        {
            get => _isGuideEnabled;
            set
            {
                if (_isGuideEnabled != value)
                {
                    _isGuideEnabled = value;
                    OnPropertyChanged(nameof(IsGuideEnabled));
                }
            }
        }


        // ========== 440V蓄电池电量属性 ==========
        private float _battery440VCapacity = 0.0f;
        public float Battery440VCapacity
        {
            get => _battery440VCapacity;
            set
            {
                float clampedValue = Math.Clamp(value, 0, 100);
                if (_battery440VCapacity != clampedValue)
                {
                    _battery440VCapacity = clampedValue;
                    OnPropertyChanged(nameof(Battery440VCapacity));
                }
            }
        }

        // ========== 110V蓄电池电量属性 ==========
        private float _battery110VCapacity = 0.0f; // 私有字段：float类型，默认0.0f
        public float Battery110VCapacity
        {
            get => _battery110VCapacity;
            set
            {
                float clampedValue = Math.Clamp(value, 0, 100);
                if (_battery110VCapacity != clampedValue)
                {
                    _battery110VCapacity = clampedValue; 
                    OnPropertyChanged(nameof(Battery110VCapacity)); 
                }
            }
        }

        public void Reset()

        {
            Is440VBatterySwitchClosed = false;
            Is480VPowerSwitchClosed = false;
            IsDC330VCircuitBreakerEnabled = false;
            Is25kWPowerFailed = false;
            Is5kWPowerFailed = false;
            IsPantographEnergized = false;
            IsPantographExtended1 = false;
            IsPantographExtended2 = false;
            IsPantographRetracted1 = true;
            IsPantographRetracted2 = true;
            IsLeviated = false;
            IsGuideEnabled = false;
            Battery440VCapacity = 0.0f;
            Battery110VCapacity = 0;
        }
    }
}
