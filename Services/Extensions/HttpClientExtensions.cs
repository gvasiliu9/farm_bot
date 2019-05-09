using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// HTTP Post Request Body as Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// HTTP Put Request Body as Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PutAsync(url, content);
        }

        /// <summary>
        /// HTTP Response Body as Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
    }
}
