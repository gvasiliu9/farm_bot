using Acr.UserDialogs;
using Entites;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class AddPlantViewModel : BaseViewModel
    {
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

        #endregion

        #region Commands

        public IMvxCommand AddCommand { get; }

        public IMvxCommand CancelCommand { get; }

        #endregion

        #region Services

        private IPlantService _plantService;

        #endregion

        public AddPlantViewModel(IPlantService plantService)
        {
            _plantService = plantService;

            AddCommand = new MvxCommand(async () => await Add());

            CancelCommand = new MvxCommand(async () => await Cancel());
        }

        private async Task Add()
        {
            try
            {
                // Create plant
                var plant = new Plant
                {
                    Name = Name,
                    Info = Info,
                    RowDistance = RowDistance,
                    SeedDepth = SeedDepth,
                    WaterQuanity = WaterQuantity,
                    IrigationsPerDay = IrigationsPerDay,
                    Duration = Duration
                };

                // Add plant
                await _plantService.AddAsync(plant);

                // Success
                Mvx.IoCProvider.Resolve<IUserDialogs>()
                    .Alert("Plant was added successfuly", "Info", "Close");
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IUserDialogs>()
                    .Alert(ex.Message, "Error", "Close");
            }
        }

        private async Task Cancel()
        {
            
        }
    }
}
