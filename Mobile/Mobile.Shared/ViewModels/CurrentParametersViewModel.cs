using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Mobile.Shared.Pages;

namespace Mobile.Shared.ViewModels
{
    public class CurrentParametersViewModel : INotifyPropertyChanged
    {
        #region Fields

        // Table
        string _plantName;
        string _description;
        string _airTemperature;
        string _soilHumidity;
        string _luminosity;
        string _sowingDate;
        string _plantsDistance;
        string _remainingTime;

        // Additional info
        string _serverIp;
        string _farmBotIp;
        string _username;
        string _email;

        #endregion

        #region Properties

        // Table
        public string PlantName
        {
            get => _plantName;
            set
            {
                _plantName = value;
                OnPropertyChanged(nameof(PlantName));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string AirTemperature
        {
            get => _airTemperature;
            set
            {
                _airTemperature = value;
                OnPropertyChanged(nameof(AirTemperature));
            }
        }

        public string SoilHumidity
        {
            get => _soilHumidity;
            set
            {
                _soilHumidity = value;
                OnPropertyChanged(nameof(SoilHumidity));
            }
        }

        public string Luminosity
        {
            get => _luminosity;
            set
            {
                _luminosity = value;
                OnPropertyChanged(nameof(Luminosity));
            }
        }

        public string SowingDate
        {
            get => _sowingDate;
            set
            {
                _sowingDate = value;
                OnPropertyChanged(nameof(SowingDate));
            }
        }

        public string PlantsDistance
        {
            get => _plantsDistance;
            set
            {
                _plantsDistance = value;
                OnPropertyChanged(nameof(PlantsDistance));
            }
        }

        public string RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged(nameof(RemainingTime));
            }
        }

        // Additional info
        public string ServerIp
        {
            get => _serverIp;
            set
            {
                _serverIp = value;
                OnPropertyChanged(nameof(ServerIp));
            }
        }

        public string FarmBotIp
        {
            get => _farmBotIp;
            set
            {
                _farmBotIp = value;
                OnPropertyChanged(nameof(FarmBotIp));
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        // Events & Commands
        public event PropertyChangedEventHandler PropertyChanged;

        public Command UpdateParametersCommand { get; } 

        #endregion;

        public CurrentParametersViewModel()
        {
            UpdateParametersCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new UpdateParametersPage());

            });
        }

        #region Events

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Methods



        #endregion
    }
}
