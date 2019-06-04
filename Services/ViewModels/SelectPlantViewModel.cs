using Acr.UserDialogs;
using Entities;
using I18NPortable;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Services.Abstractions;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Services.ViewModels
{
    public class SelectPlantViewModel: BaseViewModel
    {
        #region Fields

        private HubConnection _communicationHubConnection;

        private int _totalForSeeding;

        #endregion

        #region Bindable Properties

        ObservableCollection<Plant> _plants = new ObservableCollection<Plant>();
        public ObservableCollection<Plant> Plants { get { return _plants; } }

        private Plant _selectedItem;

        public Plant SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);

                if (_selectedItem == null)
                    return;

                ItemSelected();
            }
        }

        #endregion

        #region Services

        private readonly IPlantService _plantService;

        private readonly IFarmBotPlantsService _farmBotPlantsService;

        #endregion

        public SelectPlantViewModel(IPlantService plantService
            , IUserDialogs userDialogs
            , IFarmBotPlantsService farmBotPlantsService)
        {
            // Services
            _plantService = plantService;

            _farmBotPlantsService = farmBotPlantsService;

            UserDialogs = userDialogs;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            // Get plants
            IsBusy();

            var getPlantsResult = await _plantService.GetAllAsync();

            foreach (Plant plant in getPlantsResult)
                _plants.Add(plant);

            IsBusy(false);

            // Connect to communication hub
            try
            {
                _communicationHubConnection = CommunicationHub.Connect();

                await _communicationHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }
        }

        private void ItemSelected()
        {
            var count = UserDialogs.Prompt(new PromptConfig {
                 InputType = InputType.Number,
                  Message = "EnterPlantsNumber".Translate(),
                  OkText = I18N.Current["Start"],
                  CancelText = I18N.Current["Cancel"],
                  Title = I18N.Current["Confirmation"],
                  OnAction = async (PromptResult) => {

                      int number = 0;

                      // Parse number
                      int.TryParse(PromptResult.Text, out number);

                      _totalForSeeding = number;

                      // Start new seeding
                      if(number > 0)
                        await Apply();
                  }
            }); 
        }

        private async Task Apply()
        {
            IsBusy();

            try
            {
                var farmBotPlant = new FarmBotPlant
                {
                    FarmBotId = TempData.FarmBotId,
                    PlantId = SelectedItem.Id,
                };

                await _farmBotPlantsService.AddAsync(farmBotPlant);

                // Start seeding process
                await _communicationHubConnection.InvokeAsync("Seeding", SelectedItem, _totalForSeeding);

                // Result
                UserDialogs.Toast(I18N.Current["ProcessIsStarted"]);

                await NavigationService.Close(this);
            }
            catch(Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }

            IsBusy(false);
        }

        public async Task Delete(Plant plant)
        {
            IsBusy();

            try
            {
                await _plantService.DeleteAsync(plant.Id);

                await NavigationService.Close(this);
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }

            IsBusy(false);
        }
    }
}
