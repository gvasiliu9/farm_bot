using Acr.UserDialogs;
using I18NPortable;
using MvvmCross;
using MvvmCross.Binding;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Services.Abstractions;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Services
{
    public class MvvmCrossApp : MvxApplication
    {
        public override void Initialize()
        {
            //CreatableTypes()
            //    .EndingWith("Service")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton<IPlantService>(new PlantService());

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterAppStart<MenuViewModel>();

            I18N.Current
            .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
            .SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
            .SetThrowWhenKeyNotFound(false) // Optional: Throw an exception when keys are not found (recommended only for debugging)
            .Init(GetType().GetTypeInfo().Assembly); // assembly where locales live
        }
    }
}
