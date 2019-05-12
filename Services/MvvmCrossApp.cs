using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Binding;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Services.Abstractions;
using Services.ViewModels;
using System;
using System.Collections.Generic;
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
        }
    }
}
