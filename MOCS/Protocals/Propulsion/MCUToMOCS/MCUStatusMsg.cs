using System.Buffers.Binary;
using MOCS.Coms;

namespace MOCS.Protocals.Propulsion.MCUToMOCS
{
    public sealed class MCUStatusMsg : BaseMessage, IIncomingMsg<BaseMessage>
    {
        public static (BaseMessage? msg, string? error) Parse(ReadOnlyMemory<byte> buffer)
        {
            MCUStatusMsg? msg = null;
            string? error = null;

            var statusMsgLen = buffer.Length - 8;
            if (statusMsgLen != 164)
            {
                error = $"牵引状态报文的用户数据段长度:{statusMsgLen}不为164字节";
                return (msg, error);
            }

            var span = buffer.Span;

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(span[..2]);
            var repeat = span[2];
            var dest = span[4];
            var src = span[5];
            var partId = span[6];
            var msgId = span[7];
            var userData = buffer.Slice(8, 164);

            msg = new MCUStatusMsg
            {
                SequenceNumber = seq,
                RepeatCounter = repeat,
                Destination = dest,
                Source = src,
                PartId = partId,
                MsgId = msgId,
                UserData = userData,
            };

            return (msg, error);
        }
    }
}
