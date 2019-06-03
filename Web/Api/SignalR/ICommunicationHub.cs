using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.SignalR
{
    public interface ICommunicationHub
    {
        Task RemoteControl(string direction);

        Task Seeding(Plant plant, int count);

        Task Message(string message);
    }
}
