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
        }

        private void ForwardButton_Pressed(object sender, EventArgs e)
        {
            ViewModel.ForwardCommand.Execute();
        }

        private void LeftButton_Pressed(object sender, EventArgs e)
        {
            ViewModel.LeftCommand.Execute();
        }

        private void ForwardButton_Released(object sender, EventArgs e)
        {
            ViewModel.StopCommand.Execute();
        }

        private void LeftButton_Released(object sender, EventArgs e)
        {
            ViewModel.StopCommand.Execute();
        }

        private void RightButton_Pressed(object sender, EventArgs e)
        {
            ViewModel.RightCommand.Execute();
        }

        private void RightButton_Released(object sender, EventArgs e)
        {
            ViewModel.StopCommand.Execute();
        }

        private void BackButton_Pressed(object sender, EventArgs e)
        {
            ViewModel.BackwardCommand.Execute();
        }

        private void BackButton_Released(object sender, EventArgs e)
        {
            ViewModel.StopCommand.Execute();
        }

        private void HomeButton_Pressed(object sender, EventArgs e)
        {
            ViewModel.HomeCommand.Execute();
        }

        private void DownButton_Released(object sender, EventArgs e)
        {
            ViewModel.StopCommand.Execute();
        }

        private void DownButton_Pressed(object sender, EventArgs e)
        {
            ViewModel.DownCommand.Execute();
        }

        private void UpButton_Released(object sender, EventArgs e)
        {
            ViewModel.StopCommand.Execute();
        }

        private void UpButton_Pressed(object sender, EventArgs e)
        {
            ViewModel.UpCommand.Execute();
        }
    }
}