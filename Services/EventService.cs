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
    public class EventService : IEventService
    {
        private readonly IHttpRequest _httpRequest;

        public EventService()
        {
            _httpRequest = new HttpRequest();
        }

        public async Task AddAsync(Event farmBotEvent)
        {
            string url = $"{Endpoints.Event}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PostAsync(url, farmBotEvent);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            string url = $"{Endpoints.Event}/{id}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.DeleteAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            string url = $"{Endpoints.Event}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<IEnumerable<Event>>();
        }

        public async Task<Event> GetByIdAsync(Guid farmBotEventId)
        {
            string url = $"{Endpoints.Event}/{farmBotEventId}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.GetAsync(url);

            httpResponeMessage.EnsureSuccessStatusCode();

            return await httpResponeMessage.Content.ReadAsJsonAsync<Event>();
        }

        public async Task UpdateAsync(Event farmBotEvent)
        {
            string url = $"{Endpoints.Event}";

            HttpResponseMessage httpResponeMessage = await _httpRequest.PutAsync(url, farmBotEvent);

            httpResponeMessage.EnsureSuccessStatusCode();
        }
    }
}
