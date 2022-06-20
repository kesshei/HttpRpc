using HttpRpc.Tunnel.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRpc.Tunnel
{
    /// <summary>
    /// 通道工厂
    /// </summary>
    public static class TunnelFactory
    {
        public static BaseTunnel CreateTunnel(string ServerApiUrl, TunnelType tunnelType = TunnelType.HTTP)
        {
            BaseTunnel BaseTunnel = null;
            switch (tunnelType)
            {
                case TunnelType.HTTP:
                    {
                        BaseTunnel = new HTTPTunnelServer(ServerApiUrl);
                    }
                    break;
            }
            return BaseTunnel;
        }
    }
}
