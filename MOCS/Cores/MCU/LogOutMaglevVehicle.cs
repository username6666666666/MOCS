namespace MOCS.Cores.MCU
{
    public class LogOutMaglevVehicle
    {
        /// <summary>
        /// 悬浮架标识号
        /// </summary>
        public byte MaglevVehicleIdentifier { get; set; } = 0x01;

        public void Reset()
        {
            MaglevVehicleIdentifier = 0x01;
        }

        public byte[] ToByteArray()
        {
            Span<byte> data = stackalloc byte[2];
            data[0] = MaglevVehicleIdentifier;

            return data.ToArray();
        }
    }
}
