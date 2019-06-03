using Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.SignalR
{
    public class CommunicationHub : Hub<ICommunicationHub>
    {
        public async Task Seeding(Plant plant, int count)
        {
            await Clients.All.Seeding(plant, count);
        }

        public async Task RemoteControl (string direction)
        {
            await Clients.All.RemoteControl(direction);
        }

        public async Task Message(string message)
        {
            await Clients.All.Message(message);
        }
    }
}
