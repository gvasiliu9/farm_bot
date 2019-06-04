using Acr.UserDialogs;
using Entities;
using Microsoft.AspNetCore.SignalR;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Services.Abstractions;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private FarmBot _farmBot;
        public FarmBot FarmBot
        {
            get => _farmBot;
            set
            {
                SetProperty(ref _farmBot, value);
            }
        }

        private Plant _selectedItem;
        public Plant SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);

                if (_selectedItem == null)
                    return;

                Task.Run(async () => await UpdateParameters());
            }
        }

        public ObservableCollection<Plant> Plants { get; }
            = new ObservableCollection<Plant>();

        private bool _hasSeededPlants;
        public bool HasSeededPlants
        {
            get => _hasSeededPlants;
            set
            {
                SetProperty(ref _hasSeededPlants, value);
            }
        }

        #endregion

        #region Services

        private IParametersService _parametersService;

        private IFarmBotService _farmBotService;

        private IFarmBotPlantsService _farmBotPlantsService;

        private IPlantService _plantService;

        #endregion

        public ParametersViewModel(IFarmBotService farmBotService
            , IPlantService plantService
            , IFarmBotPlantsService farmBotPlantsService
            , IParametersService parametersService
            , IUserDialogs userDialogs)
        {
            _farmBotService = farmBotService;
            _farmBotPlantsService = farmBotPlantsService;
            _plantService = plantService;
            _parametersService = parametersService;

            UserDialogs = userDialogs;
        }

        public override async Task Initialize()
        {

            IsBusy();

            try
            {
                // Get farmbot
                FarmBot = await _farmBotService
                    .GetByIdAsync(TempData.FarmBotId);

                // Get farmbot sensors parameters
                Parameters = await _parametersService
                    .GetByIdAsync(FarmBot.Id);

                // Get farmbot plants
                var farmBotPlants = await _farmBotPlantsService.GetAllAsync();

                if (farmBotPlants.Any())
                    foreach (var farmbotplant in farmBotPlants)
                        Plants.Add(await _plantService.GetByIdAsync(farmbotplant.PlantId));

                // Checkc if has seeded plants
                HasSeededPlants = Plants.Any();
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }

            IsBusy(false);

            await base.Initialize();
        }

        private async Task UpdateParameters()
        {
            await NavigationService.Navigate<UpdateParameteresViewModel, Plant>(SelectedItem);
        }
    }
}
