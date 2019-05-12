using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Services.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        public readonly IMvxNavigationService _navigationService;

        public IMvxCommand ToParametersCommand { get; }

        public IMvxCommand ToPlantsCommand { get; }

        public IMvxCommand ToVideoCommand { get; }

        public IMvxCommand ToSettingsCommand { get; }

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            // Redirect to parameters page
            ToParametersCommand = new MvxCommand(async () =>
            {
                await _navigationService.Navigate<AddPlantViewModel>();
            });

            // Redirect to plants page
            ToPlantsCommand = new MvxCommand(async () =>
            {
                int i = 0;
            });

            // Redirect to video & controll page
            ToVideoCommand = new MvxCommand(async () =>
            {
                int i = 0;
            });

            // Redirect to video & controll page
            ToSettingsCommand = new MvxCommand(async () =>
            {
                int i = 0;
            });
        }
    }
}
