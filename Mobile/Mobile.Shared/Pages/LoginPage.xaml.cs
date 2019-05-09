using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.ViewModels;

namespace Mobile.Shared.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage () : base()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            this.BindingContext = new LoginViewModel();
        }
	}
}