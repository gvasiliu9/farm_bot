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

        private readonly IUserDialogs _userDialogs;

        private readonly IMvxNavigationService _navigationService;

        #endregion

        public SelectPlantViewModel(IUserDialogs userDialogs
            , IMvxNavigationService navigationService
            , IPlantService plantService
            , ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _settingService = settingsService;
            _plantService = plantService;

            // Get plants
            Task.Run(async() => {

                _userDialogs.ShowLoading(I18N.Current["LoadingMessage"]);

                var getPlantsResult = await _plantService.GetAllAsync();

                foreach (Plant plant in getPlantsResult)
                    _plants.Add(plant);

                _userDialogs.HideLoading();
            });
        }

        private void ItemSelected()
        {
            _userDialogs.Confirm(new ConfirmConfig
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
                _userDialogs.ShowLoading(I18N.Current["LoadingMessage"]);

                await Task.Delay(5000);

                Settings userSettings = await _settingService.GetByIdAsync(Guid.Parse("74cba66d-d231-4b70-8363-ec4a2ce4ce07"));

                userSettings.PlantId = SelectedItem.Id;

                await _settingService.UpdateAsync(userSettings);

                // Result
                _userDialogs.Toast(I18N.Current["ProcessIsStarted"]);

                await _navigationService.Close(this);
            }
            catch(Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }

            _userDialogs.HideLoading();
        }
    }
}
