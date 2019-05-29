using MvvmCross.Forms.Views;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.OpenTok.Service;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages
{
	public partial class RealtimeVideoPage : MvxContentPage<RealtimeViewModel>
	{
		public RealtimeVideoPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            CrossOpenTok.Current.ApiKey = "46335352"; // OpenTok api key from your account
            CrossOpenTok.Current.SessionId = "1_MX40NjMzNTM1Mn5-MTU1ODc0MTAyOTQ3Mn5BMXh5aWdCTUdDcFEvT2ZlT2w5QXVrZnp-UH4"; // Id of session for connecting
            CrossOpenTok.Current.UserToken = "T1==cGFydG5lcl9pZD00NjMzNTM1MiZzaWc9MDg0Y2EyNTA1Zjk1Mzk4ZTIxZDQ5NmI1NGIzNjc4NDEyMjFhNDEzYjpzZXNzaW9uX2lkPTFfTVg0ME5qTXpOVE0xTW41LU1UVTFPRGMwTVRBeU9UUTNNbjVCTVhoNWFXZENUVWREY0ZFdlQyWmxUMnc1UVhWclpucC1VSDQmY3JlYXRlX3RpbWU9MTU1ODc0MTAyNSZub25jZT02NzYwMTImcm9sZT1QVUJMSVNIRVI="; // User's token
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!CrossOpenTok.Current.TryStartSession())
            {
                return;
            }
        }
    }
}