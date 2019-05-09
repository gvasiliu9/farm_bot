using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages.Controls.Navigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BarMenu : ContentView
	{
        #region Bindable Properties

        public static readonly BindableProperty ActivePageProperty = BindableProperty
            .Create(nameof(ActivePage), typeof(string), typeof(BarMenu), default(string));

        public string ActivePage
        {
            get
            {
                return (string)GetValue(ActivePageProperty);
            }

            set
            {
                SetValue(ActivePageProperty, value);
            }
        }

        #endregion

        public BarMenu()
        {
            InitializeComponent();

            // Home icon
            var homeIconTapGesture = new TapGestureRecognizer();
            homeIconTapGesture.Tapped += (s, e) =>
            {
                Application.Current.MainPage = new NavigationPage(new MenuPage());
            };

            homeIcon.GestureRecognizers.Add(homeIconTapGesture);

            // Current paramters icon
            var currentParametersIconTapGesture = new TapGestureRecognizer();
            currentParametersIconTapGesture.Tapped += (s, e) =>
            {
                Application.Current.MainPage = new NavigationPage(new CurrentParametersPage());
            };

            currentParametersIcon.GestureRecognizers.Add(currentParametersIconTapGesture);

            // Real time video icon
            var realtimeVideoTapGesture = new TapGestureRecognizer();
            realtimeVideoTapGesture.Tapped += (s, e) =>
            {
                Application.Current.MainPage = new NavigationPage(new RealtimeVideoPage());
            };

            realtimeVideoIcon.GestureRecognizers.Add(realtimeVideoTapGesture);

            // Plants list icon
            var plantsListTapGesture = new TapGestureRecognizer();
            plantsListTapGesture.Tapped += (s, e) =>
            {
                Application.Current.MainPage = new NavigationPage(new PlantsListPage());
            };

            plantsListIcon.GestureRecognizers.Add(plantsListTapGesture);

            // Settings icon
            var settingsTapGesture = new TapGestureRecognizer();
            settingsTapGesture.Tapped += (s, e) =>
            {
                Application.Current.MainPage = new NavigationPage(new SettingsPage());
            };

            settignsIcon.GestureRecognizers.Add(settingsTapGesture);
        }

        #region Events

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ActivePageProperty.PropertyName)
            {
                if(ActivePage == AppResources.CurrentParameters)
                {
                    currentParametersIcon.IsEnabled = false;
                    currentParametersIcon.Opacity = 0.5;
                }
                if (ActivePage == AppResources.RealTimeVideo)
                {
                    realtimeVideoIcon.IsEnabled = false;
                    realtimeVideoIcon.Opacity = 0.5;
                }
                if (ActivePage == AppResources.PlantsList)
                {
                    plantsListIcon.IsEnabled = false;
                    plantsListIcon.Opacity = 0.5;
                }
                if (ActivePage == AppResources.Settings)
                {
                    settignsIcon.IsEnabled = false;
                    settignsIcon.Opacity = 0.5;
                }
            }
        }

        #endregion
    }
}