using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRpc.Tunnel
{
    /// <summary>
    /// 服务端通道
    /// 可以用http协议，socket，等通讯方式实现
    /// </summary>
    public abstract class BaseTunnel
    {
        public BaseTunnel(TunnelType TunnelType)
        {
            this.TunnelType = TunnelType;
        }
        public TunnelType TunnelType { get; private set; }
        /// <summary>
        /// 调用方法
        /// </summary>  
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract TunnelResponse Invoke(TunnelRequest request);
    }
}
