using Castle.DynamicProxy;
using HttpRpc.Tunnel;
using System;
using System.Collections.Generic;

namespace HttpRpc
{
    public class Interceptor<T> : IInterceptor
    {
        private Type InterfaceType { get; set; } = typeof(T);
        private BaseTunnel ServerTunnel;
        public Interceptor(BaseTunnel ServerTunnel)
        {
            this.ServerTunnel = ServerTunnel;
        }
        public void Intercept(IInvocation invocation)
        {
            var action = $"{InterfaceType.Namespace}-{InterfaceType.Name}-{invocation.Method.Name}";
            RPCServer.APIActions.TryGetValue(action, out var methed);
            var parameters = new Dictionary<string, object>();
            var p = invocation.Method.GetParameters();
            for (int i = 0; i < p.Length; i++)
            {
                parameters.Add(p[i].Name, invocation.Arguments[i]);
            }
            var response = ServerTunnel.Invoke(new TunnelRequest() { RemoteMethed = methed.path, Parameters = parameters, ReturnType = invocation.Method.ReturnType, HttpRequestMethod = methed.httpMethod });
            if (response.StateCode != 200)
            {
                throw new Exception(response.Msg);
            }
            invocation.ReturnValue = response.Body;
        }
    }
}
