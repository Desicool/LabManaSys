using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EndPointJob
{
    public class ResponseType
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; }
        public bool IsSuccessCode { get; set; }
    }
    public static class RpcWrapper
    {
        public static HttpClient client = new HttpClient();
        public static int Port { get; set; } = 10001;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">must start with '/'</param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string CallServiceByPost(string url, string jsonString)
        {
            string uri = $"https://localhost:{Port}{url}";
            var content = new StringContent(jsonString);
            content.Headers.ContentType.MediaType = "application/json";
            var res = client.PutAsync(uri, content).Result;
            if (res.IsSuccessStatusCode)
            {
                return res.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return res.ReasonPhrase;
            }
        }

    }
}
