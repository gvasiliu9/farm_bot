using MvvmCross.Forms.Views;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPlantPage : MvxContentPage<AddPlantViewModel>
	{
		public AddPlantPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

        }
	}
}