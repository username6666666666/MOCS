namespace MOCS.Cores.MCU
{
    public class MCUReply
    {
        /// <summary>
        /// 接收通道1状态
        /// </summary>
        public ChannelRecvStatusEnum Channel1RecvStatus { get; set; } =
            ChannelRecvStatusEnum.Normal;

        /// <summary>
        /// 接收通道2状态
        /// </summary>
        public ChannelRecvStatusEnum Channel2RecvStatus { get; set; } =
            ChannelRecvStatusEnum.Normal;

        /// <summary>
        /// 牵引应答报文错误标识号
        /// </summary>
        public MCUReplyErrorIdentifierEnum MCUReplyErrorIdentifier { get; set; } =
            MCUReplyErrorIdentifierEnum.NoError;

        /// <summary>
        /// 需要MOCS重复发送以前报文的数量
        /// </summary>
        public byte MsgNumToRepeat { get; set; } = 0;

        /// <summary>
        /// 处理状态
        /// </summary>
        public MCUProcessStatusEnum MCUProcessStatus { get; set; } = MCUProcessStatusEnum.None;

        /// <summary>
        /// 应答的报文的报文的标识号
        /// </summary>
        public byte ResponseMsgId { get; set; } = 0x01;

        // TODO: 请求相关的其他数据

        public void Reset()
        {
            Channel1RecvStatus = ChannelRecvStatusEnum.Normal;

            Channel2RecvStatus = ChannelRecvStatusEnum.Normal;

            MCUReplyErrorIdentifier = MCUReplyErrorIdentifierEnum.NoError;

            MsgNumToRepeat = 0;

            MCUProcessStatus = MCUProcessStatusEnum.None;

            ResponseMsgId = 0x01;
        }
    }
}
