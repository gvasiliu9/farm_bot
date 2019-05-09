using Entites;
using Services.Abstractions;
using Services.Extensions;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class FarmBotService : IFarmBotService
    { 
        private readonly IHttpRequest _httpRequest;

        public FarmBotService(IHttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public async Task AddAsync(FarmBot farmBot)
        {
            string url = $"{Endpoints.FarmBot}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PostAsync(url, farmBot);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            string url = $"{Endpoints.FarmBot}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.DeleteAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<FarmBot>> GetAllAsync()
        {
            string url = $"{Endpoints.FarmBot}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<IEnumerable<FarmBot>>();
        }

        public async Task<FarmBot> GetByIdAsync(Guid id)
        {
            string url = $"{Endpoints.FarmBot}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<FarmBot>();
        }

        public async Task UpdateAsync(FarmBot farmBot)
        {
            string url = $"{Endpoints.FarmBot}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PutAsync(url, farmBot);

            httpResponeMessage.EnsureSuccessStatusCode();
        }
    }
}
