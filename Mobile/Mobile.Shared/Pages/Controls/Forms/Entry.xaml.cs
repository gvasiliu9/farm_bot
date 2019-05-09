using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages.Controls.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Entry : ContentView
	{
        #region Bindable Properties

        public static readonly BindableProperty TextProperty = BindableProperty
            .Create(nameof(Text), typeof(string), typeof(Entry), default(string));

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

        public static readonly BindableProperty PlaceholderProperty = BindableProperty
            .Create(nameof(Placeholder), typeof(string), typeof(Entry), default(string));

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty
            .Create(nameof(BorderColor), typeof(Color), typeof(Entry), default(Color));

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }

            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty
            .Create(nameof(TextColor), typeof(Color), typeof(Entry), default(Color));

        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }

            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty
            .Create(nameof(PlaceholderColor), typeof(Color), typeof(Entry), default(Color));

        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }

            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty
            .Create(nameof(BackgroundColor), typeof(Color), typeof(Entry), default(Color));

        public new Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }

            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        #endregion

        public Entry ()
		{
			InitializeComponent ();
		}

        #region Events

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                entry.Text = Text;
            }
            if (propertyName == PlaceholderProperty.PropertyName)
            {
                entry.Placeholder = Placeholder;
            }
            if (propertyName == BorderColorProperty.PropertyName)
            {
                entryContainer.BorderColor = BorderColor;
            }
            if (propertyName == TextColorProperty.PropertyName)
            {
                entry.TextColor = TextColor;
            }
            if (propertyName == PlaceholderColorProperty.PropertyName)
            {
                entry.PlaceholderColor = PlaceholderColor;
            }
            if (propertyName == BackgroundColorProperty.PropertyName)
            {
                entryContainer.BackgroundColor = BackgroundColor;
            }
        }

        #endregion
    }
}