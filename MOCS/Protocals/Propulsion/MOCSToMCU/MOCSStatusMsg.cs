namespace MOCS.Protocals.Propulsion.MOCSToMCU
{
    public sealed class MOCSStatusMsg : BaseSendMsg
    {
        public MOCSStatusMsg()
        {
            MsgId = 0x01;
        }
    }
}
