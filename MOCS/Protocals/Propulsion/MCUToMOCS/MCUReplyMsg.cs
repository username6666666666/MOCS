using System.Buffers.Binary;
using MOCS.Coms;

namespace MOCS.Protocals.Propulsion.MCUToMOCS
{
    public sealed class MCUReplyMsg : BaseMessage, IIncomingMsg<BaseMessage>
    {
        public static (BaseMessage? msg, string? error) Parse(ReadOnlyMemory<byte> buffer)
        {
            MCUReplyMsg? msg = null;
            string? error = null;

            var replyMsgLen = buffer.Length - 8;
            if (replyMsgLen != 12)
            {
                error = $"牵引应答报文的用户数据段长度:{replyMsgLen}不为12字节";
                return (msg, error);
            }

            var span = buffer.Span;

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(span[..2]);
            var repeat = span[2];
            var dest = span[4];
            var src = span[5];
            var partId = span[6];
            var msgId = span[7];
            var userData = buffer.Slice(8, 12);

            msg = new MCUReplyMsg
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
