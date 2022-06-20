using Newtonsoft.Json;
using System;

namespace Common
{
    /// <summary>
    /// JsonHelper
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 序列化成json 字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJsonStr(this object o)
        {
            return JsonConvert.SerializeObject(o, new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            });
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <returns></returns>
        public static T ToObject<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <returns></returns>
        public static object ToObject(this string jsonStr, Type type)
        {
            return JsonConvert.DeserializeObject(jsonStr, type);
        }
    }
}
