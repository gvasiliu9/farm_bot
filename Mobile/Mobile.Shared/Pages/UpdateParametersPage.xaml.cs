﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateParametersPage : ContentPage
	{
		public UpdateParametersPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent ();

            Title = AppResources.UpdateParameters;
		}
	}
}