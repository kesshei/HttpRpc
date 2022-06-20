using HttpRpc.Tunnel.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRpc.Tunnel
{
    /// <summary>
    /// 通道请求参数
    /// </summary>
    public class TunnelRequest
    {
        /// <summary>
        ///  远程方法
        /// </summary>
        public string RemoteMethed { get; set; }
        /// <summary>
        /// 请求方法类型
        /// </summary>
        public HttpRequestMethodType HttpRequestMethod { get; set; }
        /// <summary>
        /// 参数信息
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }
        /// <summary>
        /// 返回的对象类型
        /// </summary>
        public Type ReturnType { get; set; }
    }
}
