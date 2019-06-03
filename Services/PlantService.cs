using Entities;
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
    public class PlantService : IPlantService
    {
        private readonly IHttpRequest _httpRequest;

        public PlantService()
        {
            _httpRequest = new HttpRequest();
        }

        public async Task AddAsync(Plant plant)
        {
            string url = $"{Endpoints.Plant}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PostAsync(url, plant);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            string url = $"{Endpoints.Plant}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.DeleteAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            string url = $"{Endpoints.Plant}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<IEnumerable<Plant>>();
        }

        public async Task<Plant> GetByIdAsync(Guid id)
        {
            string url = $"{Endpoints.Plant}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<Plant>();
        }

        public async Task UpdateAsync(Plant plant)
        {
            string url = $"{Endpoints.Plant}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PutAsync(url, plant);

            httpResponeMessage.EnsureSuccessStatusCode();
        }
    }
}
