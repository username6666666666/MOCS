using System.Buffers.Binary;

namespace MOCS.Cores.MCU
{
    public class LogInMaglevVehicle
    {
        /// <summary>
        /// 悬浮架标识
        /// </summary>
        public byte MaglevVehicleIdentifier { get; set; } = 0x01;

        /// <summary>
        /// 悬浮架类型
        /// </summary>
        public MaglevVehicleTypeEnum MaglevVehicleType { get; set; } =
            MaglevVehicleTypeEnum.MaglevFrame;

        /// <summary>
        /// MOCS编号
        /// </summary>
        public byte MOCSID { get; set; } = 0x01;

        /// <summary>
        /// 悬浮架方向
        /// </summary>
        public MaglevVehicleDirectionEnum MaglevVehicleDirection { get; set; } =
            MaglevVehicleDirectionEnum.Unknown;

        /// <summary>
        /// 当前悬浮架位置
        /// </summary>
        public int CurrentVehiclePos { get; set; } = 0;

        /// <summary>
        /// 悬浮架长度
        /// </summary>
        public int VehicleLength { get; set; } = 0;

        /// <summary>
        /// 最大线路允许速度
        /// </summary>
        public short VelocityLimit { get; set; } = 0;

        public void Reset()
        {
            MaglevVehicleIdentifier = 0x01;

            MaglevVehicleType = MaglevVehicleTypeEnum.MaglevFrame;

            MOCSID = 0x01;

            MaglevVehicleDirection = MaglevVehicleDirectionEnum.Unknown;

            CurrentVehiclePos = 0;

            VehicleLength = 0;

            VelocityLimit = 0;
        }

        public byte[] ToByteArray()
        {
            Span<byte> data = stackalloc byte[14];

            data[0] = MaglevVehicleIdentifier;
            data[1] = (byte)MaglevVehicleType;
            data[2] = MOCSID;
            data[3] = (byte)MaglevVehicleDirection;
            BinaryPrimitives.WriteInt32LittleEndian(data.Slice(4, 4), CurrentVehiclePos);
            BinaryPrimitives.WriteInt32LittleEndian(data.Slice(8, 4), VehicleLength);
            BinaryPrimitives.WriteInt16LittleEndian(data.Slice(12, 2), VelocityLimit);

            return data.ToArray();
        }
    }
}
