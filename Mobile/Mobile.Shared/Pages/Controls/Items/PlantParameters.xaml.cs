using I18NPortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Pages.Controls
{
	public partial class PlantParameters : ContentView
	{
        #region Bindable Properties

        public static readonly BindableProperty NameProperty = BindableProperty
            .Create(nameof(Name), typeof(string), typeof(PlantParameters), default(string));

        public string Name
        {
            get
            {
                return (string)GetValue(NameProperty);
            }

            set
            {
                SetValue(NameProperty, value);
            }
        }

        public static readonly BindableProperty InfoProperty = BindableProperty
            .Create(nameof(Info), typeof(string), typeof(PlantParameters), default(string));

        public string Info
        {
            get
            {
                return (string)GetValue(InfoProperty);
            }

            set
            {
                SetValue(InfoProperty, value);
            }
        }

        public static readonly BindableProperty RowDistanceProperty = BindableProperty
            .Create(nameof(RowDistance), typeof(string), typeof(PlantParameters), default(string));

        public string RowDistance
        {
            get
            {
                return (string)GetValue(RowDistanceProperty);
            }

            set
            {
                SetValue(RowDistanceProperty, value);
            }
        }

        public static readonly BindableProperty PlantDistanceProperty = BindableProperty
            .Create(nameof(PlantDistance), typeof(string), typeof(PlantParameters), default(string));

        public string PlantDistance
        {
            get
            {
                return (string)GetValue(PlantDistanceProperty);
            }

            set
            {
                SetValue(PlantDistanceProperty, value);
            }
        }

        public static readonly BindableProperty SeedDepthProperty = BindableProperty
            .Create(nameof(SeedDepth), typeof(string), typeof(PlantParameters), default(string));

        public string SeedDepth
        {
            get
            {
                return (string)GetValue(SeedDepthProperty);
            }

            set
            {
                SetValue(SeedDepthProperty, value);
            }
        }

        public static readonly BindableProperty SoilHumidityProperty = BindableProperty
            .Create(nameof(SoilHumidity), typeof(string), typeof(PlantParameters), default(string));

        public string SoilHumidity
        {
            get
            {
                return (string)GetValue(SoilHumidityProperty);
            }

            set
            {
                SetValue(SoilHumidityProperty, value);
            }
        }

        public static readonly BindableProperty DurationProperty = BindableProperty
            .Create(nameof(Duration), typeof(string), typeof(PlantParameters), default(string));

        public string Duration
        {
            get
            {
                return (string)GetValue(DurationProperty);
            }

            set
            {
                SetValue(DurationProperty, value);
            }
        }

        #endregion

        public PlantParameters ()
		{
			InitializeComponent ();

            // Init parameter name labels
            parameterNameLabel.Text = "Parameter".Translate();
            measurementUnitLabel.Text = "MeasurementUnit".Translate();
            value.Text = "Value".Translate();
            rowDistanceLabel.Text = "RowDistance".Translate();
            plantDistanceLabel.Text = "PlantsDistance".Translate();
            seedDepthLabel.Text = "SeedDepth".Translate();
            soilHumidityLabel.Text = "SoilHumidity".Translate();
            durationLabel.Text = "Duration".Translate();
            updateLink.Text = "UpdateParameters".Translate();

            // Init values labels
            rowDistanceUnit.Text = "Cm".Translate();
            plantDistanceUnit.Text = "Cm".Translate();
            seedDepthUnit.Text = "Cm".Translate();
            durationUnit.Text = "Days".Translate();

            // Init values
            info.Text = "NoInfo".Translate();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Name
            if (propertyName == NameProperty.PropertyName)
            {
                name.Text = Name;
            }

            // Info
            if (propertyName == InfoProperty.PropertyName)
            {
                info.Text = Info;
            }

            // Row distance
            if (propertyName == RowDistanceProperty.PropertyName)
            {
                rowDistance.Text = "" + RowDistance;
            }

            // Plant distance
            if (propertyName == PlantDistanceProperty.PropertyName)
            {
                plantDistance.Text = "" + PlantDistance;
            }

            // Seed depth
            if (propertyName == SeedDepthProperty.PropertyName)
            {
                seedDepth.Text = "" + SeedDepth;
            }

            // Soil humidity
            if (propertyName == SoilHumidityProperty.PropertyName)
            {
                soilHumidity.Text = "" + SoilHumidity;
            }

            // Duration
            if (propertyName == DurationProperty.PropertyName)
            {
                duration.Text = "" + Duration;
            }
        }

        #region Methods

        #region Events

        #endregion

        #endregion
    }
}