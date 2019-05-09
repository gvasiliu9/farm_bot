using Entites;
using Services.Abstractions;
using Services.Extensions;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IHttpRequest _httpRequest;

        public SettingsService(IHttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public async Task AddAsync(Settings settings)
        {
            string url = $"{Endpoints.Settings}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PostAsync(url, settings);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            string url = $"{Endpoints.Settings}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.DeleteAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Settings>> GetAllAsync()
        {
            string url = $"{Endpoints.Settings}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<IEnumerable<Settings>>();
        }

        public async Task<Settings> GetByIdAsync(Guid id)
        {
            string url = $"{Endpoints.Settings}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<Settings>();
        }

        public async Task UpdateAsync(Settings settings)
        {
            string url = $"{Endpoints.Settings}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PutAsync(url, settings);

            httpResponeMessage.EnsureSuccessStatusCode();
        }
    }
}
