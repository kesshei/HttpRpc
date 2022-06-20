using Common;
using HttpRpc;
using Model;
using System;

namespace WebAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "HTTP RPC Test by 蓝创精英团队";
            var TestApi = RPCServer.GetServerce<TestApi>("http://localhost:5000");
            //http://localhost:10234/home/getresult?name=123
            var getresult = TestApi.GetResult(DateTime.Now.ToString());
            Console.WriteLine($"Get请求返回:{getresult}");

            //http://localhost:10234/home/result
            var result = TestApi.Result(new TestModel() { Name = "456", Age = 18, Address = "蓝创精英团队", Msg = "" });
            Console.WriteLine($"Post请求返回:{result.ToJsonStr()}");
            Console.WriteLine("测试 HttpRpc 完毕!");
            Console.ReadLine();
        }
    }
}
