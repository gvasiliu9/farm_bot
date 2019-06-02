using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers
{
    public static class CommunicationHub
    {
        public static HubConnection Connect()
        {
            return new HubConnectionBuilder()
                    .WithUrl("https://farmbotapi.azurewebsites.net/communicationhub")
                    .Build();

        }
    }
}
