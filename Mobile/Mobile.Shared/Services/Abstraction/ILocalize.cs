﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mobile.Shared.Services.Abstraction
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
