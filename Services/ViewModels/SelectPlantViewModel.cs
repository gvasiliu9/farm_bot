using Acr.UserDialogs;
using Entites;
using I18NPortable;
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
        #region Properties

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
            _settingService = settingsService;
            _plantService = plantService;

            UserDialogs = userDialogs;

            // Get plants
            Task.Run(async() => {

                IsBusy();

                var getPlantsResult = await _plantService.GetAllAsync();

                foreach (Plant plant in getPlantsResult)
                    _plants.Add(plant);

                IsBusy(false);
            });
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
            try
            {
                // Apply
                IsBusy();

                await Task.Delay(5000);

                Settings userSettings = await _settingService.GetByIdAsync(Guid.Parse("74cba66d-d231-4b70-8363-ec4a2ce4ce07"));

                userSettings.PlantId = SelectedItem.Id;

                await _settingService.UpdateAsync(userSettings);

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
