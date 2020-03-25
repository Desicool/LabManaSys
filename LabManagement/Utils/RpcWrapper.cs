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
        /// <exception cref="Exception">http call fail</exception>
        public static string CallServiceWithResult(string url,params string[] querys)
        {
            string uri = $"https://localhost:10001{url}?";
            foreach(var query in querys)
            {
                uri += query + '&';
            }            
            var res = client.GetAsync(new Uri(uri)).Result;
            if(res.IsSuccessStatusCode)
            {
                return res.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("https call failed");
            }
        }
    }
}
