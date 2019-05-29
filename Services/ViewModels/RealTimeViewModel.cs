using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class RealtimeViewModel : BaseViewModel
    {
        #region Fields

        private HubConnection _communicationHubConnection;

        #endregion

        #region Services

        #endregion

        public RealtimeViewModel()
        {

        }

        #region Methods

        public override async Task Initialize()
        {
            await base.Initialize();

            try
            {
                // Connect to communication hub
                _communicationHubConnection = new HubConnectionBuilder()
                    .WithUrl("https://farmbotapi.azurewebsites.net/communicationhub")
                    .Build();

                await _communicationHubConnection.StartAsync();

                _communicationHubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    UserDialogs.Toast(message);
                });
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }
        }

        #endregion
    }
}
