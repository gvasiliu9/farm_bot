using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Services.ViewModels;
using MvvmCross.Forms.Views;

namespace Mobile.Shared.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParametersPage : MvxContentPage<ParametersViewModel>
	{
		public ParametersPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent ();
        }
    }
}