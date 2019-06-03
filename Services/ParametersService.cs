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
    public class ParametersService : IParametersService
    {
        private readonly IHttpRequest _httpRequest;

        public ParametersService()
        {
            _httpRequest = _httpRequest = new HttpRequest();
        }

        public async Task AddAsync(Parameters parameters)
        {
            string url = $"{Endpoints.Parameters}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PostAsync(url, parameters);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid farmBotId)
        {
            string url = $"{Endpoints.Parameters}/{farmBotId}";

            HttpResponseMessage httpResponeMessage =  await _httpRequest.DeleteAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Parameters>> GetAllAsync()
        {
            string url = $"{Endpoints.Parameters}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<IEnumerable<Parameters>>();
        }

        public async Task<Parameters> GetByIdAsync(Guid farmBotId)
        {
            string url = $"{Endpoints.Parameters}/{farmBotId}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<Parameters>();
        }

        public async Task UpdateAsync(Parameters parameters)
        {
            string url = $"{Endpoints.Parameters}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PutAsync(url, parameters);

            httpResponeMessage.EnsureSuccessStatusCode();
        }
    }
}
