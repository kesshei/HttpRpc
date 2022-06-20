using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpRpc
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class InterceptorSelector : IInterceptorSelector
    {
        /// <summary>
        /// 
        /// </summary>
        public InterceptorSelector() { }
        /// <summary>
        /// 只需要处理public的方法，其他类型的不处理
        /// </summary>
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (method.IsPublic)
            {
                return interceptors;
            }
            else
            {
                return null;
            }
        }
    }
}
