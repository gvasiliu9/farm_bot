using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Label : ContentView
	{
        #region Bindable Properties

        public static readonly BindableProperty IconProperty = BindableProperty
            .Create(nameof(Icon), typeof(string), typeof(MenuItem), default(string));

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
            .Create(nameof(Text), typeof(string), typeof(MenuItem), default(string));

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

        public static readonly BindableProperty CountProperty = BindableProperty
            .Create(nameof(Count), typeof(string), typeof(MenuItem), default(string));

        public string Count
        {
            get
            {
                return (string)GetValue(CountProperty);
            }

            set
            {
                SetValue(CountProperty, value);
            }
        }

        #endregion

        public Label ()
		{
			InitializeComponent ();
		}

        #region Events

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IconProperty.PropertyName)
            {
                labelIcon.Text = Icon;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                labelText.Text = Text;
            }

            if (propertyName == CountProperty.PropertyName)
            {
                labelCount.Text = " - " + Count;
            }
        }

        #endregion
    }
}