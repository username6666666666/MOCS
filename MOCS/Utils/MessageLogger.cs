using System;
using System.Drawing;
using System.Windows.Forms;

namespace MOCS.Utils
{
    /// <summary>
    /// 报文日志输出工具 —— 线程安全地将收发报文写入 RichTextBox
    /// </summary>
    public static class MessageLogger
    {
        /// <summary>
        /// 追加一条接收报文日志到指定 RichTextBox
        /// </summary>
        public static void AppendRecv(RichTextBox rtb, string source, byte[] rawBytes, string? msgType = null)
        {
            Append(rtb, "← 接收", source, rawBytes, msgType, Color.Teal);
        }

        /// <summary>
        /// 追加一条发送报文日志到指定 RichTextBox
        /// </summary>
        public static void AppendSend(RichTextBox rtb, string source, byte[] rawBytes, string? msgType = null)
        {
            Append(rtb, "→ 发送", source, rawBytes, msgType, Color.DodgerBlue);
        }

        private static void Append(RichTextBox rtb, string direction, string source, byte[] rawBytes, string? msgType, Color baseColor)
        {
            if (rtb == null || rawBytes == null || rawBytes.Length == 0)
                return;

            var now = DateTime.Now.ToString("HH:mm:ss.fff");
            var hex = BitConverter.ToString(rawBytes);
            var len = rawBytes.Length;
            // 协议帧结构：[head 1B][len 2B][seq 2B][msgId 1B][userdata...][crc 2B][tail 1B]
            // msgId 在索引 8 处（0-based）
            var msgId = rawBytes.Length >= 9
                ? $"0x{rawBytes[8]:X2}"
                : "??";

            var header = $"[{now}] {direction} | {source} | MsgId={msgId} | Len={len}B";
            if (!string.IsNullOrEmpty(msgType))
                header += $" | {msgType}";
            header += "\r\n";
            var body = $"    {hex}\r\n\r\n";

            // 线程安全写入
            if (rtb.InvokeRequired)
            {
                rtb.BeginInvoke(new Action(() =>
                {
                    WriteToRTB(rtb, header, body, baseColor);
                }));
            }
            else
            {
                WriteToRTB(rtb, header, body, baseColor);
            }
        }

        private static void WriteToRTB(RichTextBox rtb, string header, string body, Color baseColor)
        {
            try
            {
                // 限制最大行数，防止内存溢出
                if (rtb.Lines.Length > 5000)
                {
                    rtb.Clear();
                }

                rtb.SelectionStart = rtb.TextLength;
                rtb.SelectionLength = 0;
                rtb.SelectionColor = baseColor;
                rtb.AppendText(header);

                rtb.SelectionColor = Color.Black;
                rtb.AppendText(body);

                rtb.ScrollToCaret();
            }
            catch
            {
                // 忽略 RichTextBox 已被释放等情况
            }
        }
    }
}
