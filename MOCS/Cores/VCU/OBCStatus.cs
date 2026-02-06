using System;
using System.ComponentModel;

namespace MOCS.Cores.VCU
{
    public class OBCStatus: INotifyPropertyChanged
    {
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
        public event PropertyChangedEventHandler? PropertyChanged;

        //  触发事件的辅助方法
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Is480VPowerSwitchClosed { get; set; } = false;
        public bool IsDC330VCircuitBreakerEnabled { get; set; } = false;
        public bool Is25kWPowerFailed { get; set; } = false;
        public bool Is5kWPowerFailed { get; set; } = false;
        public bool IsPantographEnergized { get; set; } = false;
        public bool IsPantographExtended1 { get; set; } = false;
        public bool IsPantographExtended2 { get; set; } = false;
        public bool IsPantographRetracted1 { get; set; } = true;
        public bool IsPantographRetracted2 { get; set; } = true;
        public bool IsLeviated { get; set; } = false;
        public bool IsGuideEnabled { get; set; } = false;
        
        // ========== 440V蓄电池电量属性 ==========
        private float _battery440VCapacity = 0.0f; // 私有字段：存储电量（0~100%）
        public float Battery440VCapacity
        {
            get => _battery440VCapacity;
            set
            {
                // 先限制电量范围（0~100%，避免异常值）
                float clampedValue = Math.Clamp(value, 0, 100);
                // 只有值真的变化时，才更新+触发事件
                if (_battery440VCapacity != clampedValue)
                {
                    _battery440VCapacity = clampedValue;
                    // 触发事件，通知UI“440V电量变了”
                    OnPropertyChanged(nameof(Battery440VCapacity));
                }
            }
        }

        public short Battery110VCapacity { get; set; } = 0;

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
