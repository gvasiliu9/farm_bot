using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mobile.Shared.Pages;

namespace Xamarin.Forms.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        private string username;

        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command LoginCommand { get; }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public bool IsBusy { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        async Task Login()
        {
            Application.Current.MainPage = new NavigationPage(new MenuPage());
        }
    }
}
