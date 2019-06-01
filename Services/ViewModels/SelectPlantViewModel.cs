using Acr.UserDialogs;
using Entites;
using I18NPortable;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Services.Abstractions;
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

        private readonly ISettingsService _settingService;

        private readonly IPlantService _plantService;

        #endregion

        public SelectPlantViewModel(IPlantService plantService
            , ISettingsService settingsService, IUserDialogs userDialogs)
        {
            // Services
            _settingService = settingsService;
            _plantService = plantService;

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
                _communicationHubConnection = new HubConnectionBuilder()
                    .WithUrl("https://farmbotapi.azurewebsites.net/communicationhub")
                    .Build();

                await _communicationHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }
        }

        private void ItemSelected()
        {
            UserDialogs.Confirm(new ConfirmConfig
            {
                Title = I18N.Current["Confirmation"],
                Message = I18N.Current["StartProcessQuestion"],
                OkText = I18N.Current["Start"],
                CancelText = I18N.Current["Cancel"],
                OnAction = async (isConfirmed) =>
                {
                    if (isConfirmed)
                        await Apply();
                }
            });
        }

        private async Task Apply()
        {
            IsBusy();

            try
            {
                // Update settings
                Settings userSettings = await _settingService
                    .GetByIdAsync(Guid.Parse("74cba66d-d231-4b70-8363-ec4a2ce4ce07"));

                userSettings.PlantId = SelectedItem.Id;

                await _settingService.UpdateAsync(userSettings);

                // Start seeding process
                await _communicationHubConnection.InvokeAsync("Seeding", SelectedItem);

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
    }
}
