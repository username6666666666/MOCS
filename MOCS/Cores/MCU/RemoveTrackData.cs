using System.Buffers.Binary;

namespace MOCS.Cores.MCU
{
    public class RemoveTrackData
    {
        /// <summary>
        /// KI标识号
        /// </summary>
        public ushort KIIdentifier { get; set; } = 0;

        /// <summary>
        /// 删除标识范围
        /// </summary>
        public RemoveFlagEnum RemoveFlag { get; set; } = RemoveFlagEnum.Specified;

        /// <summary>
        /// 静态公里标，从该处沿线路数据方向删除所有线路数据
        /// </summary>
        public int MarkPos { get; set; } = 0;

        public byte[] ToByteArray()
        {
            Span<byte> data = stackalloc byte[8];

            BinaryPrimitives.WriteUInt16LittleEndian(data[..2], KIIdentifier);
            data[2] = (byte)RemoveFlag;
            BinaryPrimitives.WriteInt32LittleEndian(data.Slice(3, 4), MarkPos);

            return data.ToArray();
        }
    }
}
