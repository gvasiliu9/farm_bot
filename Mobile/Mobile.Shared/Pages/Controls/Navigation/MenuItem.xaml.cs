using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages.Controls.Navigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuItem : ContentView
	{
        #region Bindable Properties

        public static readonly BindableProperty IconProperty = BindableProperty
            .Create(nameof(Icon), typeof(string), typeof(MenuItem), default(string), Xamarin.Forms.BindingMode.OneWayToSource);

        public string Icon
        {
            get
            {
                return (string)GetValue(IconProperty);
            }

            set
            {
                SetValue(IconProperty, value);
            }
        }

        public static readonly BindableProperty TextProperty = BindableProperty
            .Create(nameof(Text), typeof(string), typeof(MenuItem), default(string), Xamarin.Forms.BindingMode.OneWayToSource);

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(MenuItem), null);

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(MenuItem), null);

        public object CommandParameter
        {
            get { return (object) GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        #endregion

        public MenuItem ()
		{
			InitializeComponent ();

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    if(Command != null && Command.CanExecute(CommandParameter))
                    {
                        Command.Execute(CommandParameter);
                    }
                })
            });
		}

        #region Events

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IconProperty.PropertyName)
            {
                menuItemIcon.Text = Icon;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                menuItemText.Text = Text;
            }
        }

        #endregion
    }
}