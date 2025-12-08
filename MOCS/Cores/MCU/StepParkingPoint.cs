using System.Buffers.Binary;

namespace MOCS.Cores.MCU
{
    public class StepParkingPoint
    {
        /// <summary>
        /// KI标识号
        /// </summary>
        public ushort KIIdentifier { get; set; } = 0;

        /// <summary>
        /// 悬浮架标识号
        /// </summary>
        public byte MaglevVehicleIdentifier { get; set; } = 0x01;

        public void Reset()
        {
            KIIdentifier = 0;
            MaglevVehicleIdentifier = 0x01;
        }

        public byte[] ToByteArray()
        {
            Span<byte> data = stackalloc byte[4];

            BinaryPrimitives.WriteUInt16LittleEndian(data[..2], KIIdentifier);
            data[2] = MaglevVehicleIdentifier;

            return data.ToArray();
        }
    }
}
