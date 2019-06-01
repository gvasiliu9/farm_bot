using Acr.UserDialogs;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class RealtimeViewModel : BaseViewModel
    {
        #region Fields

        public enum Direction
        {
            Left,
            Forward,
            Right,
            Backward,
            Up,
            Down,
            Home,
            Stop
        }

        private HubConnection _communicationHubConnection;

        #endregion

        #region Services

        #endregion

        #region Commands

        public IMvxAsyncCommand LeftCommand { get; set; }
        
        public IMvxAsyncCommand ForwardCommand { get; set; }

        public IMvxAsyncCommand RightCommand { get; set; }

        public IMvxAsyncCommand BackwardCommand { get; set; }

        public IMvxAsyncCommand HomeCommand { get; set; }

        public IMvxAsyncCommand StopCommand { get; set; }

        public IMvxAsyncCommand UpCommand { get; set; }

        public IMvxAsyncCommand DownCommand { get; set; }

        #endregion

        public RealtimeViewModel(IUserDialogs userDialogs)
        {
            UserDialogs = userDialogs;

            LeftCommand = new MvxAsyncCommand(Left);
            ForwardCommand = new MvxAsyncCommand(Forward);
            RightCommand = new MvxAsyncCommand(Right);
            BackwardCommand = new MvxAsyncCommand(Backward);
            HomeCommand = new MvxAsyncCommand(Home);
            StopCommand = new MvxAsyncCommand(Stop);
            UpCommand = new MvxAsyncCommand(Up);
            DownCommand = new MvxAsyncCommand(Down);
        }

        #region Methods

        public override async Task Initialize()
        {
            await base.Initialize();

            // Connect to communication hub
            try
            {
                _communicationHubConnection = new HubConnectionBuilder()
                    .WithUrl("https://farmbotapi.azurewebsites.net/communicationhub")
                    .Build();

                await _communicationHubConnection.StartAsync();

                //_communicationHubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                //{
                //    UserDialogs.Toast(message);
                //});
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }
        }

        private async Task Left()
        {
            await RemoteControlCommand(Direction.Left);
        }

        private async Task Forward()
        {
            await RemoteControlCommand(Direction.Forward);
        }

        private async Task Right()
        {
            await RemoteControlCommand(Direction.Right);
        }

        private async Task Backward()
        {
            await RemoteControlCommand(Direction.Backward);
        }

        private async Task Home()
        {
            await RemoteControlCommand(Direction.Home);
        }

        private async Task Stop()
        {
            await RemoteControlCommand(Direction.Stop);
        }

        private async Task Up()
        {
            await RemoteControlCommand(Direction.Up);
        }

        private async Task Down()
        {
            await RemoteControlCommand(Direction.Down);
        }

        private async Task RemoteControlCommand(Direction direction )
        {
            try
            {
                await _communicationHubConnection.InvokeAsync("RemoteControl", direction.ToString());
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }
        }

        #endregion
    }
}
