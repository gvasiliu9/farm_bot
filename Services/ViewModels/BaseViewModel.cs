using Acr.UserDialogs;
using I18NPortable;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        public II18N Translate => I18N.Current;

        #region Services

        protected IMvxNavigationService NavigationService { get; }

        protected IUserDialogs UserDialogs { get; set; }

        #endregion

        public BaseViewModel()
        {
            // Inject services
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        #region Methods

        protected void IsBusy(bool state = true)
        {
            if (state)
                UserDialogs.ShowLoading(I18N.Current["LoadingMessage"], MaskType.Gradient);
            else
                UserDialogs.HideLoading();
        }

        #endregion
    }
}
