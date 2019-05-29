using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Services.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Services

        #endregion

        #region Commands

        public IMvxCommand ToParametersCommand { get; }

        public IMvxCommand ToPlantsCommand { get; }

        public IMvxCommand ToVideoCommand { get; }

        public IMvxCommand ToSettingsCommand { get; }

        #endregion

        public MenuViewModel()
        {
            // Redirect to parameters page
            ToParametersCommand = new MvxCommand(async () =>
            {
                await NavigationService.Navigate<ParametersViewModel>();
            });

            // Redirect to plants page
            ToPlantsCommand = new MvxCommand(async () =>
            {
                await NavigationService.Navigate<AddPlantViewModel>();
            });

            // Redirect to video & controll page
            ToVideoCommand = new MvxCommand(async () =>
            {
                await NavigationService.Navigate<RealtimeViewModel>();
            });

            // Redirect to video & controll page
            ToSettingsCommand = new MvxCommand(async () =>
            {
                await NavigationService.Navigate<AddPlantViewModel>();
            });
        }
    }
}
