using HttpRpc;
using HttpRpc.Tunnel.HTTP;
using HttpRpc.WebApi;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITest
{
    /// <summary>
    /// 服务端API
    /// </summary>
    [APIServer("Home")]
    public interface TestApi : IApiServer
    {
        /// <summary>
        /// 获取结果
        /// </summary>
        [APIServer(HttpRequestMethodType.GET, "GetResult")]
        public string GetResult(string name);
        /// <summary>
        /// 获取结果
        /// </summary>
        [APIServer(HttpRequestMethodType.POST, "Result")]
        public TestModel Result(TestModel testModel);
    }
}
