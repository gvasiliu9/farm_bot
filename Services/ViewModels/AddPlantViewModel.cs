using Acr.UserDialogs;
using Entites;
using FluentValidation.Results;
using I18NPortable;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Services.Abstractions;
using Services.Validations;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class AddPlantViewModel : BaseViewModel
    {
        #region Bindable Properties

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private string _info;
        public string Info
        {
            get => _info;
            set
            {
                SetProperty(ref _info, value);
            }
        }

        private int _rowDistance;
        public int RowDistance
        {
            get => _rowDistance;
            set
            {
                SetProperty(ref _rowDistance, value);
            }
        }

        private int _plantDistance;
        public int PlantDistance
        {
            get => _plantDistance;
            set
            {
                SetProperty(ref _plantDistance, value);
            }
        }

        private short _seedDepth;
        public short SeedDepth
        {
            get => _seedDepth;
            set
            {
                SetProperty(ref _seedDepth, value);
            }
        }

        private byte _soilHumidity;
        public byte SoilHumidity
        {
            get => _soilHumidity;
            set
            {
                SetProperty(ref _soilHumidity, value);
            }
        }

        private short _duration;
        public short Duration
        {
            get => _duration;
            set
            {
                SetProperty(ref _duration, value);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand AddCommand { get; }

        public IMvxCommand CancelCommand { get; }

        #endregion

        #region Services

        private readonly IPlantService _plantService;

        #endregion

        public AddPlantViewModel(IPlantService plantService, IUserDialogs userDialogs)
        {
            UserDialogs = userDialogs;

            _plantService = plantService;

            AddCommand = new MvxCommand(async () => await Add());

            CancelCommand = new MvxCommand(async () => await Cancel());
        }

        #region Methods

        private async Task Add()
        {
            try
            {
                // Loading
                IsBusy();

                var plant = new Plant
                {
                    Name = _name,
                    Info = _info,
                    RowDistance = _rowDistance,
                    PlantDistance = _plantDistance,
                    SeedDepth = _seedDepth,
                    SoilHumidity = _soilHumidity,
                    Duration = _duration
                };

                // Add plant
                await _plantService.AddAsync(plant);

                // Success
                UserDialogs.Toast(I18N.Current["ItemAdded"]);
            }
            catch (Exception ex)
            {
                UserDialogs.Toast(ex.Message);
            }

            IsBusy(false);
        }

        private async Task Cancel()
        {
            await NavigationService.Close(this);
        }

        #region Events

        #endregion

        #endregion
    }
}
