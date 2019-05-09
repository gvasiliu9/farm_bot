using Xamarin.Forms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.Shared.ViewModels;

namespace Mobile.Shared.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlantsListPage : ContentPage
	{
		public PlantsListPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            this.Title = AppResources.PlantsList;

            BindingContext = new PlantsListViewModel();
        }
    }
}