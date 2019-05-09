using Mobile.Shared.Services.Abstraction;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Shared.Extensions
{
    // You exclude the 'Extension' suffix when using in XAML
    [ContentProperty("Key")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci = null;
        const string ResourceId = "Mobile.Shared.AppResources";

        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
            () => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string Key { get; set; }

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key == null)
                return string.Empty;

            var translation = ResMgr.Value.GetString(Key, ci);
            if (translation == null)
            {
                #if DEBUG

                    throw new ArgumentException(
                        string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Key, ResourceId, ci.Name),
                        "Key");

                #else

                    translation = Key; // HACK: returns the key, which GETS DISPLAYED TO THE USER

                #endif
            }
            return translation;
        }
    }
}
