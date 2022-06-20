using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRpc.Tunnel
{
    /// <summary>
    /// 返回的信息
    /// </summary>
    public class TunnelResponse
    {
        /// <summary>
        /// 响应的消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 响应的状态码
        /// 200为正常
        /// </summary>
        public int StateCode { get; set; }
        /// <summary>
        /// 数据结果
        /// </summary>
        public object Body { get; set; }
    }
}
