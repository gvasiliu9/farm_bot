using Acr.UserDialogs;
using I18NPortable;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private string _defaultUsername = "ati.utm";
        private string _defaultPassword = "12345";

        #endregion

        #region Bindable Properties

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

        #endregion

        #region Commands

        public IMvxAsyncCommand LoginCommand { get; set; } 

        #endregion

        public LoginViewModel()
        {
            LoginCommand = new MvxAsyncCommand(Login);

            Username = _defaultUsername;
            Password = _defaultPassword;
        }

        public async Task Login()
        {
            if (Username == _defaultUsername && Password == _defaultPassword)
                await NavigationService.Navigate<MenuViewModel>();
        }
    }
}
