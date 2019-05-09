using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mobile.Shared.Pages;

namespace Mobile.Shared.ViewModels
{
    public class PlantsListViewModel: INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SelectPlantCommand { get; }

        public Command AddPlantCommand { get; }

        //public string Property
        //{
        //    get => field;
        //    set
        //    {
        //        field = value;
        //        OnPropertyChanged(nameof(Property));
        //    }
        //}

        public bool IsBusy { get; set; }

        #endregion

        public PlantsListViewModel()
        {
            AddPlantCommand = new Command(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(new AddPlantPage());
            });

            SelectPlantCommand = new Command(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(new SelectPlantPage());
            });
        }

        #region Events

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
