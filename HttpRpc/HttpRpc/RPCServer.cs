using Castle.DynamicProxy;
using HttpRpc.Tunnel;
using HttpRpc.Tunnel.HTTP;
using HttpRpc.WebApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HttpRpc
{
    /// <summary>
    /// PRC服务
    /// </summary>
    public static class RPCServer
    {
        private static ProxyGenerator ProxyGenerator = new ProxyGenerator();
        private static InterceptorSelector InterceptorSelector = new InterceptorSelector();
        static RPCServer()
        {
            InitServer();//初始化加载
        }
        /// <summary>
        /// 方法与远程方法的映射
        /// </summary>
        public static ConcurrentDictionary<string, (string path, HttpRequestMethodType httpMethod)> APIActions = new();
        /// <summary>
        /// 创建默认代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T Proxy<T>(BaseTunnel BaseTunnel = null) where T : class
        {
            if (BaseTunnel == null)
            {
                throw new ArgumentNullException(nameof(BaseTunnel));
            }
            var inter = new Interceptor<T>(BaseTunnel);
            return ProxyGenerator.CreateInterfaceProxyWithoutTarget<T>(new ProxyGenerationOptions() { Selector = InterceptorSelector }, inter);
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        public static T GetServerce<T>(string ServerApiUrl, BaseTunnel BaseTunnel = null) where T : class
        {
            BaseTunnel ??= TunnelFactory.CreateTunnel(ServerApiUrl, TunnelType.HTTP);
            return Proxy<T>(BaseTunnel);
        }
        public static void InitServer()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetInterface(nameof(IApiServer)) != null && type.IsInterface)
                    {
                        var typeName = type.Name;
                        var typeAttribute = type.GetCustomAttributes(false).Where(t => t is APIServerAttribute).Select(t => t as APIServerAttribute).ToList().FirstOrDefault();
                        var TypeNames = new List<string>();
                        if (typeAttribute != null && typeAttribute.GetActions()?.Any() == true)
                        {
                            TypeNames.AddRange(typeAttribute.GetActions());
                        }
                        else
                        {
                            TypeNames.Add(typeName);
                        }
                        foreach (var methodInfo in type.GetMethods())
                        {
                            var first = methodInfo.GetCustomAttributes(false).Where(t => t is APIServerAttribute).Select(t => t as APIServerAttribute).FirstOrDefault();
                            if (first != null && first.GetActions()?.Any() == true)
                            {
                                var path = APIServerAttribute.GetAction(TypeNames, first.GetActions());
                                var value = (path, first.HttpRequestMethod);
                                APIActions.AddOrUpdate($"{type.Namespace}-{typeName}-{methodInfo.Name}", value, (k, v) => v = (path, first.HttpRequestMethod));
                            }
                        }
                    }
                }
            }
        }
    }
}
