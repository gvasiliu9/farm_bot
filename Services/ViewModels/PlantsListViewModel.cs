using Acr.UserDialogs;
using I18NPortable;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class PlantsListViewModel: BaseViewModel
    {
        #region Properties

        private string _count;
        public string Count
        {
            get => _count;
            set { SetProperty(ref _count, value); }
        } 

        #endregion

        #region Commands

        public IMvxCommand AddPlantCommand { get; } 

        public IMvxCommand SelectPlantCommand { get; }

        #endregion

        #region Services

        private readonly IPlantService _plantService;

        private readonly IUserDialogs _userDialogs;

        private readonly IMvxNavigationService _navigationService;

        #endregion

        public PlantsListViewModel(IMvxNavigationService navigationService
            , IPlantService plantService
            , IUserDialogs userDialogs)
        {
            _plantService = plantService;

            _userDialogs = userDialogs;

            _navigationService = navigationService;

            AddPlantCommand = new MvxCommand(async () => await AddPlant());

            SelectPlantCommand = new MvxCommand(async () => await SelectPlant());

            // Get plants count
            Task.Run(async () =>
            {
                _userDialogs.ShowLoading(I18N.Current["LoadingMessage"]);

                Count = "" + _plantService.GetAllAsync().Result.ToList().Count();

                _userDialogs.HideLoading();
            });
        }

        #region Methods

        #region Events

        private async Task AddPlant()
        {
            await _navigationService.Navigate<AddPlantViewModel>();
        }

        private async Task SelectPlant()
        {
            await _navigationService.Navigate<SelectPlantViewModel>();
        }

        #endregion

        #endregion
    }
}
