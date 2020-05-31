using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LabManagement.Utils
{
    public static class HttpWrapper
    {
        public static HttpClient client = new HttpClient();
        public static int Port { get; set; } = 10001;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">must start with '/'</param>
        /// <returns></returns>
        /// <exception cref="Exception">http call fail</exception>
        public static ResponseType CallServiceByGet(string url,params string[] querys)
        {
            string uri = $"https://localhost:{Port}{url}?";
            foreach (var query in querys)
            {
                uri += query + '&';
            }
            var res = client.GetAsync(new Uri(uri)).Result;
            var body = res.Content.ReadAsStringAsync().Result;
            return new ResponseType
            {
                StatusCode = res.StatusCode,
                Body = body,
                IsSuccessCode = res.IsSuccessStatusCode,
            };
        }
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
            //string uri = $"https://localhost:44398{url}";
            var content = new StringContent(jsonString);
            content.Headers.ContentType.MediaType="application/json";
            var res = client.PostAsync(uri,content).Result;
            if (res.IsSuccessStatusCode)
            {
                return res.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("https call failed");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">must start with '/'</param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void CallServiceByPostAsync(string url, int port, string jsonString)
        {
            string uri = $"https://localhost:{port}{url}";
            //string uri = $"https://localhost:44398{url}";
            var content = new StringContent(jsonString);
            content.Headers.ContentType.MediaType = "application/json";
            client.PostAsync(uri, content);
        }

    }
}
