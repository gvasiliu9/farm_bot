using Acr.UserDialogs;
using Entities;
using I18NPortable;
using MvvmCross.Commands;
using Services.Abstractions;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Fields

        private FarmBot _farmBot;

        #endregion

        #region Bindable Properties

        private string _ipCameraAddress;
        public string IpCameraAddress
        {
            get => _ipCameraAddress;
            set
            {
                SetProperty(ref _ipCameraAddress, value);
            }
        }

        private string _farmBotIp;
        public string FarmBotIp
        {
            get => _farmBotIp;
            set
            {
                SetProperty(ref _farmBotIp, value);
            }
        }

        private string _farmBotName;
        public string FarmBotName
        {
            get => _farmBotName;
            set
            {
                SetProperty(ref _farmBotName, value);
            }
        }

        #endregion

        #region Services

        private readonly IFarmBotService _farmBotService;

        #endregion

        #region Commands

        public MvxAsyncCommand UpdateCommand { get; set; }

        public MvxAsyncCommand CancelCommand { get; set; }

        #endregion

        public SettingsViewModel(IUserDialogs userDialogs
            ,IFarmBotService farmBotService)
        {
            UserDialogs = userDialogs;

            _farmBotService = farmBotService;

            // Init commands
            UpdateCommand = new MvxAsyncCommand(Update);
            CancelCommand = new MvxAsyncCommand(Cancel);
        }

        #region Methods

        public override async Task Initialize()
        {
            try
            {
                IsBusy();

                // Get settings
                _farmBot = await _farmBotService
                    .GetByIdAsync(TempData.FarmBotId);

                FarmBotName = _farmBot.Name;
                FarmBotIp = _farmBot.IpAddress;
                IpCameraAddress = _farmBot.IpCameraAddress;

                // Result
                IsBusy(false);
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }

            await base.Initialize();
        }

        private async Task Update()
        {
            try
            {
                IsBusy();

                // Get settings
                FarmBot farmBot = await _farmBotService
                    .GetByIdAsync(TempData.FarmBotId);

                // Update
                farmBot.Name = FarmBotName;
                farmBot.IpAddress = FarmBotIp;
                farmBot.IpCameraAddress = IpCameraAddress;

                await _farmBotService.UpdateAsync(farmBot);

                // Result
                IsBusy(false);

                UserDialogs.Toast("SettingsUpdated".Translate());
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }
        }

        private async Task Cancel()
        {
            await NavigationService.Close(this);
        }

        #region Events

        #endregion

        #endregion
    }
}
