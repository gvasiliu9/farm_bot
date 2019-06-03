using Acr.UserDialogs;
using Entities;
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
    public class UpdateParameteresViewModel : BaseViewModel<Plant>
    {
        #region Bindable Properties

        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

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

        public IMvxCommand UpdateCommand { get; }

        public IMvxCommand CancelCommand { get; }

        #endregion

        #region Services

        private readonly IPlantService _plantService;

        #endregion

        public UpdateParameteresViewModel(IPlantService plantService, IUserDialogs userDialogs)
        {
            UserDialogs = userDialogs;

            _plantService = plantService;

            UpdateCommand = new MvxCommand(async () => await Update());

            CancelCommand = new MvxCommand(async () => await Cancel());
        }

        #region Methods

        public override void Prepare()
        {
        }

        public override void Prepare(Plant plant)
        {
            Id = plant.Id;
            Name = plant.Name;
            Info = plant.Info;
            RowDistance = plant.RowDistance;
            PlantDistance = plant.PlantDistance;
            SeedDepth = plant.SeedDepth;
            SoilHumidity = plant.SoilHumidity;
            Duration = plant.Duration;
        }

        private async Task Update()
        {
            try
            {
                // Loading
                IsBusy();

                var plant = new Plant
                {
                    Id = _id,
                    Name = _name,
                    Info = _info,
                    RowDistance = _rowDistance,
                    PlantDistance = _plantDistance,
                    SeedDepth = _seedDepth,
                    SoilHumidity = _soilHumidity,
                    Duration = _duration
                };

                // Add plant
                await _plantService.UpdateAsync(plant);

                // Success
                UserDialogs.Toast(I18N.Current["ItemUpdated"]);
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
