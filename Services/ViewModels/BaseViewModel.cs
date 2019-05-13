using I18NPortable;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        public II18N Translate => I18N.Current;
    }
}
