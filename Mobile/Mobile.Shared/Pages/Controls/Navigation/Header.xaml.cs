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
	public partial class Header : ContentView
	{
        #region Bindable Properties

        public static readonly BindableProperty PageTitleProperty = BindableProperty
            .Create(nameof(PageTitle), typeof(string), typeof(Header), default(string));

        public string PageTitle
        {
            get
            {
                return (string)GetValue(PageTitleProperty);
            }

            set
            {
                SetValue(PageTitleProperty, value);
            }
        }

        #endregion

        public Header ()
		{
			InitializeComponent ();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                // Logout
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            };

            logoutLabel.GestureRecognizers.Add(tapGestureRecognizer);
		}

        #region Events

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == PageTitleProperty.PropertyName)
            {
                pageTitleLabel.Text = PageTitle;
            }
        }

        #endregion
    }
}