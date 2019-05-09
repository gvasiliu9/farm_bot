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
	public partial class MenuPage : ContentPage
	{
        public Command CurrentParametersCommand;

		public MenuPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            // Redirect to current parameters page
            currentParamsPageLink.Command = new Command(async () => {
                await Navigation.PushAsync(new CurrentParametersPage());
            });

            // Redirect to plats list page
            plantsListPageLink.Command = new Command( async () => {
                await Navigation.PushAsync(new PlantsListPage());
            });

            // Redirect to real time video page
            realTimeVideoPageLink.Command = new Command(async () => {
                await Navigation.PushAsync(new RealtimeVideoPage());
            });

            // Redirect to setting page
            settingsPageLink.Command = new Command(async () => {
                await Navigation.PushAsync(new SettingsPage());
            });
        }
	}
}