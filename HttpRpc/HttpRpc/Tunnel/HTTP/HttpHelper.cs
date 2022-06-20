using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace HttpRpc.Tunnel.HTTP
{
    /// <summary>
    /// http 请求方法类型
    /// </summary>
    public enum HttpRequestMethodType
    {
        GET,
        POST
    }
    /// <summary>
    /// HTTPRequest
    /// </summary>
    public static class HttpHelper
    {
        private static int TimeOut = 60;
        private static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(TimeOut) };
        /// <summary>
        /// get方式获取数据
        /// </summary>
        public static (string data, int httpState, string errMsg) Get(string url, Dictionary<string, object> data = null)
        {
            if (data != null)
            {
                url = PretreatmentRequestParameters(url, data);
            }
            return HttpRequest(url, null, methodType: HttpRequestMethodType.GET, null, Encoding.UTF8);
        }
        /// <summary>
        /// get方式获取数据
        /// </summary>
        public static (string data, int httpState, string errMsg) Post(string url, object data = null, string ContentType = "application/json")
        {
            string postdata = string.Empty;
            if (data != null)
            {
                postdata = JsonHelper.ToJsonStr(data);
            }
            return HttpRequest(url, postdata, methodType: HttpRequestMethodType.POST, ContentType: ContentType);
        }
        /// <summary>
        /// http请求
        /// </summary>
        private static (string data, int httpState, string errMsg) HttpRequest(string url, string postData = null, HttpRequestMethodType methodType = HttpRequestMethodType.GET, string ContentType = null, Encoding encoding = null)
        {
            string Result = null;
            int httpState = 500;
            string errMsg = "请求失败!";
            encoding ??= Encoding.UTF8;
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.RequestUri = new Uri(url);
                if (methodType == HttpRequestMethodType.GET)
                {
                    httpRequestMessage.Method = HttpMethod.Get;
                }
                else
                {
                    httpRequestMessage.Method = HttpMethod.Post;
                    httpRequestMessage.Content = new StringContent(postData, Encoding.UTF8, "application/json");
                }
                var HttpResponseMessage = httpClient.Send(httpRequestMessage);
                httpState = (int)HttpResponseMessage.StatusCode;
                if (HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result = HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    errMsg = "";
                }
                else
                {
                    errMsg = $"状态码不对:{httpState}";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return (Result, httpState, errMsg);
        }
        /// <summary>
        /// 参数预处理
        /// </summary>
        private static string PretreatmentRequestParameters(string Url, Dictionary<string, object> parameters)
        {
            string parameterData = null;
            if (parameters?.Any() == true)
            {
                List<string> parameter = new List<string>();
                foreach (string key in parameters.Keys)
                {
                    if (parameters[key] != null || !parameters[key].Equals(""))
                    {
                        parameter.Add(string.Format("{0}={1}", key, HttpUtility.UrlEncode(parameters[key].ToString())));
                    }
                }
                if (parameter.Any())
                {
                    parameterData = string.Join("&", parameter);
                }
            }
            if (string.IsNullOrWhiteSpace(parameterData))
            {
                return null;
            }
            if (Url?.Contains("?") == true)
            {
                if (Url?.Contains("=") == true)
                {
                    Url = $"{Url}&{parameterData}";
                }
                else
                {
                    Url = $"{Url}{parameterData}";
                }
            }
            else
            {
                Url = $"{Url}?{parameterData}";
            }
            return Url;
        }
    }
}
