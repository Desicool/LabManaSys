using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LabManagement.Utils
{
    public static class RpcWrapper
    {
        public static HttpClient client = new HttpClient();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">must start with '/'</param>
        /// <returns></returns>
        public static string CallServiceWithResult(string url)
        {
            var res = client.GetStringAsync(new Uri($"https://localhost:10001{url}"));
            return res.Result;
        }
    }
}
