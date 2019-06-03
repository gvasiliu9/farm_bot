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
    public class FarmBotPlantsService : IFarmBotPlantsService
    {
        private readonly IHttpRequest _httpRequest;

        public FarmBotPlantsService()
        {
            _httpRequest = new HttpRequest();
        }

        public async Task AddAsync(FarmBotPlant farmBotEvent)
        {
            string url = $"{Endpoints.FarmBotPlants}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PostAsync(url, farmBotEvent);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            string url = $"{Endpoints.FarmBotPlants}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.DeleteAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<FarmBotPlant>> GetAllAsync()
        {
            string url = $"{Endpoints.FarmBotPlants}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<IEnumerable<FarmBotPlant>>();
        }

        public async Task<FarmBotPlant> GetByIdAsync(Guid farmBotId)
        {
            string url = $"{Endpoints.FarmBotPlants}/{farmBotId}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<FarmBotPlant>();
        }

        public Task UpdateAsync(FarmBotPlant entity)
        {
            throw new NotImplementedException();
        }
    }
}
