using System.Configuration;

namespace MOCS.Cores.MCU
{
    public class ChangeTractionState
    {
        /// <summary>
        /// 要求切换到的状态
        /// </summary>
        public TargetStateEnum TargetState { get; set; } = TargetStateEnum.Initial;

        public void Reset()
        {
            TargetState = TargetStateEnum.Initial;
        }

        public byte[] ToByteArray()
        {
            Span<byte> data = stackalloc byte[2];

            data[0] = (byte)TargetState;

            return data.ToArray();
        }
    }
}
