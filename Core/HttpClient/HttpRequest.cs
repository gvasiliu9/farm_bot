using Entites;
using Services.Extensions;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HttpRequest : IHttpRequest
    {
        private readonly HttpClient _httpClient;

        public HttpRequest()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri(Endpoints.BaseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            _httpClient.Timeout = TimeSpan.FromSeconds(2500);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(url);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object body)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient
                .PostAsJsonAsync(url, body);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> PutAsync(string url, object body)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient
                .PutAsJsonAsync(url, body);

            return httpResponseMessage;
        }
    }
}
