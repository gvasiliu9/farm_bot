using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.SignalR
{
    public interface ICommunicationHub
    {
        Task SendMessage(string user, string message);

        Task ReceiveMessage(string user, string message);

        Task ReceiveMessage(string message);
    }
}
