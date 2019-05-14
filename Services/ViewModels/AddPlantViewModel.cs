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
        #region Fields

        private Plant _plant;

        private PlantValidator _validator;

        #endregion

        #region Properties

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _info;
        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        private int _rowDistance;
        public int RowDistance
        {
            get => _rowDistance;
            set => SetProperty(ref _rowDistance, value);
        }

        private short _seedDepth;
        public short SeedDepth
        {
            get => _seedDepth;
            set => SetProperty(ref _seedDepth, value);
        }

        private byte _waterQuantity;
        public byte WaterQuantity
        {
            get => _waterQuantity;
            set => SetProperty(ref _waterQuantity, value);
        }

        private byte _irigationsPerDay;
        public byte IrigationsPerDay
        {
            get => _irigationsPerDay;
            set => SetProperty(ref _irigationsPerDay, value);
        }

        private short _duration;
        public short Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        #endregion

        #region Commands

        public IMvxCommand AddCommand { get; }

        public IMvxCommand CancelCommand { get; }

        #endregion

        #region Services

        private readonly IPlantService _plantService;

        private readonly IUserDialogs _userDialogs;

        private readonly IMvxNavigationService _navigationService;

        #endregion

        public AddPlantViewModel(IMvxNavigationService navigationService
            , IPlantService plantService
            , IUserDialogs userDialogs)
        {
            _plantService = plantService;

            _userDialogs = userDialogs;

            _validator = new PlantValidator();

            _navigationService = navigationService;

            AddCommand = new MvxCommand(async () => await Add());

            CancelCommand = new MvxCommand(async () => await Cancel());
        }

        #region Methods

        private async Task Add()
        {
            try
            {
                // Loading
                _userDialogs.ShowLoading(I18N.Current["LoadingMessage"], MaskType.Clear);

                // Add plant
                await _plantService.AddAsync(_plant);

                _userDialogs.HideLoading();

                // Success
                _userDialogs.Toast(I18N.Current["ItemAdded"]);
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
                _userDialogs.HideLoading();
            }
        }

        private async Task Cancel()
        {
            await _navigationService.Close(this);
        }

        #region Events

        public override Task RaisePropertyChanged([CallerMemberName] string whichProperty = "")
        {
            // Create plant
            _plant = new Plant
            {
                Name = Name,
                Info = Info,
                RowDistance = RowDistance,
                SeedDepth = SeedDepth,
                WaterQuanity = WaterQuantity,
                IrigationsPerDay = IrigationsPerDay,
                Duration = Duration
            };

            // Validate
            IsValid = _validator.Validate(_plant).IsValid;

            return base.RaisePropertyChanged(whichProperty);
        }

        #endregion

        #endregion
    }
}
