using HttpRpc.Tunnel.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRpc
{
    public class APIServerAttribute : Attribute
    {
        private List<string> Actons = new List<string>();
        public APIServerAttribute(params string[] Actions)
        {
            if (Actions?.Any() == true)
            {
                Actons.AddRange(Actions);
            }
            this.HttpRequestMethod = HttpRequestMethodType.GET;
        }
        public APIServerAttribute(HttpRequestMethodType HttpRequestMethod, params string[] Actions)
        {
            if (Actions?.Any() == true)
            {
                Actons.AddRange(Actions);
            }
            this.HttpRequestMethod = HttpRequestMethod;
        }
        public HttpRequestMethodType HttpRequestMethod { get; set; }
        /// <summary>
        /// 获取所有动作
        /// </summary>
        /// <returns></returns>
        public List<string> GetActions()
        {
            return Actons;
        }
        public static string GetAction(List<string> action1, List<string> action2)
        {
            var action = new List<string>(action1);
            action.AddRange(action2);
            return string.Join("/", action);
        }
    }
}
