using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Libraly.Helper
{
    public static class ApiHelper
    {
        public static HttpClient client;

        static ApiHelper()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public static Task<T> ConvertResponse<T>(HttpResponseMessage response)
        {
            if (response != null && response.IsSuccessStatusCode)
            {
                var t = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                return Task.FromResult(t);
            }

            return null;
        }

        public static async Task<HttpResponseMessage> HttpGet(string url, string token = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await client.GetAsync(url);
            }
            catch (System.Exception ex)
            {
                HttpResponseMessage re = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };

                return await Task.FromResult(re);
            }
        }

        public static async Task<HttpResponseMessage> HttpPost<T>(string url, T obj, string token = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = JsonConvert.SerializeObject(obj);

                StringContent data = new StringContent(content, Encoding.UTF8, "application/json");

                return await client.PostAsync(url, data);
            }
            catch (System.Exception ex)
            {
                var re = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };

                return await Task.FromResult(re);
            }
        }

        public static async Task<HttpResponseMessage> HttpPut<T>(string url, T obj, string token = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = JsonConvert.SerializeObject(obj);

                var data = new StringContent(content, Encoding.UTF8, "application/json");

                return await client.PutAsync(url, data);
            }
            catch (System.Exception ex)
            {
                var re = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };

                return await Task.FromResult(re);
            }

        }

        public static async Task<HttpResponseMessage> HttpDelete(string url, string token = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await client.DeleteAsync(url);
            }
            catch (System.Exception ex)
            {
                var re = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };

                return await Task.FromResult(re);
            }
        }
    }
}
