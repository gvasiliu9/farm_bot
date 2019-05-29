using Acr.UserDialogs;
using Entites;
using Microsoft.AspNetCore.SignalR;
using MvvmCross.Commands;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class ParametersViewModel: BaseViewModel
    {
        #region Bindable properties

        private Parameters _parameters;
        public Parameters Parameters
        {
            get => _parameters;
            set
            {
                SetProperty(ref _parameters, value);
            }
        }

        private Plant _plant;
        public Plant Plant
        {
            get => _plant;
            set
            {
                SetProperty(ref _plant, value);
            }
        }

        private FarmBot _farmBot;
        public FarmBot FarmBot
        {
            get => _farmBot;
            set
            {
                SetProperty(ref _farmBot, value);
            }
        }

        private int _remainingTime;
        public int RemainingTime
        {
            get => _remainingTime;
            set
            {
                SetProperty(ref _remainingTime, value);
            }
        }

        private string _seedingDate;
        public string SeedingDate
        {
            get => _seedingDate;
            set
            {
                SetProperty(ref _seedingDate, value);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand UpdateCommand { get; }

        #endregion

        #region Services

        private IParametersService _parametersService;

        private IFarmBotService _farmBotService;

        private IPlantService _plantService;

        private ISettingsService _settingsService;

        #endregion

        public ParametersViewModel(IFarmBotService farmBotService
            , ISettingsService settingsService
            , IPlantService plantService
            , IParametersService parametersService
            , IUserDialogs userDialogs)
        {
            _farmBotService = farmBotService;
            _settingsService = settingsService;
            _plantService = plantService;
            _parametersService = parametersService;

            UserDialogs = userDialogs;

            // Update parameters command
            UpdateCommand = new MvxAsyncCommand(async () => {

            });
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            IsBusy();

            try
            {
                // Get farmbot
                FarmBot = await _farmBotService
                    .GetByIdAsync(Guid.Parse("99d9742b-1ee2-45c9-a9fb-8742baa3bb86"));

                // Get farmbot sensors parameters
                Parameters = await _parametersService
                    .GetByIdAsync(FarmBot.Id);

                // Get farmbot settings
                Settings farmBotSettings = await _settingsService
                    .GetByIdAsync(Guid.Parse("74cba66d-d231-4b70-8363-ec4a2ce4ce07"));

                // Get plant
                Plant = await _plantService.GetByIdAsync(farmBotSettings.PlantId);

                // Convert seeding date to string
                SeedingDate = Plant.Created.ToShortDateString();

                // Calc remaining time
                RemainingTime = (DateTime.Now - Plant.Created).Days;
            }
            catch(Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }

            IsBusy(false);
        }
    }
}
