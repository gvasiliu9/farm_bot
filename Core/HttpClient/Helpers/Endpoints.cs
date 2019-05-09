﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers
{
    public static class Endpoints
    {

        #if DEBUG

            public static string BaseAddress { get; set; } = "http://localhost:58347/api/";

        #else

            public static string BaseAddress { get; set; } = "https://farmbotapi.azurewebsites.net/api/";

        #endif

        public static string Parameters { get; } = "parameters";

        public static string Plant { get; } = "plant";

        public static string FarmBot { get; } = "farmbot";

        public static string Settings { get; } = "settings";

        public static string Event { get; } = "event";
    }
}
