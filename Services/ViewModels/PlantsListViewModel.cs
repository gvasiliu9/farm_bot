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

        #endregion

        public PlantsListViewModel(IPlantService plantService, IUserDialogs userDialogs)
        {
            _plantService = plantService;

            UserDialogs = userDialogs;

            AddPlantCommand = new MvxCommand(async () => await AddPlant());

            SelectPlantCommand = new MvxCommand(async () => await SelectPlant());
        }

        #region Methods

        #region Events

        private async Task AddPlant()
        {
            await NavigationService.Navigate<AddPlantViewModel>();
        }

        private async Task SelectPlant()
        {
            await NavigationService.Navigate<SelectPlantViewModel>();
        }

        #endregion

        #endregion
    }
}
