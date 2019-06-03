using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mobile.Shared;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Platforms.Android.Core;
using Services;
using Acr.UserDialogs;
using Xamarin.Forms.OpenTok.Android.Service;
using Xamarin.Forms;

namespace Mobile.Android
{
    [Activity(Label = "Farm Bot", 
        Icon = "@mipmap/ic_launcher", 
        Theme = "@style/MainTheme.Base", 
        MainLauncher = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<MvvmCrossApp, App>, MvvmCrossApp, App>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Forms.SetFlags("CollectionView_Experimental");

            base.OnCreate(savedInstanceState);

            UserDialogs.Init(this);

            PlatformOpenTokService.Init();
        }
    }
}