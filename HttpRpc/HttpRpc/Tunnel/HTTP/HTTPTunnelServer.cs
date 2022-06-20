using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRpc.Tunnel.HTTP
{
    /*
     默认按照json格式来
     */
    public class HTTPTunnelServer : BaseTunnel
    {
        private string ServerApiUrl;
        public HTTPTunnelServer(string ServerApiUrl) : base(TunnelType.HTTP)
        {
            this.ServerApiUrl = ServerApiUrl;
        }
        public override TunnelResponse Invoke(TunnelRequest request)
        {
            var souse = new TaskCompletionSource<TunnelResponse>();
            Task.Run(() =>
            {
                try
                {
                    var serverUrl = $"{ServerApiUrl}/{request.RemoteMethed}";
                    (string data, int httpState, string errMsg) result;
                    if (request.HttpRequestMethod == HttpRequestMethodType.GET)
                    {
                        result = HttpHelper.Get(serverUrl, request.Parameters);
                    }
                    else
                    {
                        result = HttpHelper.Post(serverUrl, request.Parameters.FirstOrDefault().Value);
                    }
                    if (result.httpState != 200)
                    {
                        throw new Exception(result.errMsg);
                    }
                    var body = Process(result.data, request);
                    souse.SetResult(new TunnelResponse() { StateCode = result.httpState, Body = body, Msg = result.errMsg });
                }
                catch (Exception e)
                {
                    souse.SetResult(new TunnelResponse() { StateCode = 500, Body = null, Msg = e.Message.ToString() });
                }
            });
            return souse.Task.Result;
        }
        private object Process(string data, TunnelRequest request)
        {
            if (string.IsNullOrWhiteSpace(data) || request.ReturnType == typeof(void))
            {
                return null;
            }
            if (request.ReturnType == typeof(string))
            {
                return data;
            }
            return JsonHelper.ToObject(data, request.ReturnType);
        }
    }
}
