using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MOCS.Cores.VCU
{
    public class VSPSInfo:INotifyPropertyChanged
    {
        #region 单例模式
        private static readonly VSPSInfo _instance = new VSPSInfo();
        public static VSPSInfo Instance => _instance;
        
        /// <summary>
        /// 私有构造函数，防止外部 new
        /// </summary>
        private VSPSInfo() { }
        #endregion

        #region 私有字段
        private ushort _life = 0;
        private bool _forward = true;
        private ushort _relativePos = 0;
        private ushort _speed = 0;
        #endregion

        #region 公共属性
        public ushort Life
        {
            get => _life;
            set
            {
                if (_life != value)
                {
                    _life = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 方向 true-正向 false-反向
        /// </summary>
        public bool Forward
        {
            get => _forward;
            set
            {
                if (_forward != value)
                {
                    _forward = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 相对位置 单位: mm
        /// </summary>
        public ushort RelativePos
        {
            get => _relativePos;
            set
            {
                if (_relativePos != value)
                {
                    _relativePos = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 速度 单位: cm/s
        /// </summary>
        public ushort Speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region 接口实现（属性变更通知）
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 触发属性变更通知
        /// </summary>
        /// <param name="propertyName">属性名（自动获取）</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Reset()
        {
            Life = 0;
            Forward = true;
            RelativePos = 0;
            Speed = 0;
        }
    }
}
