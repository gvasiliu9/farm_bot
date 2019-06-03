using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers
{
    public static class Endpoints
    {
        //public static string BaseAddress { get; set; } = "http://localhost:58347/api/";

        public static string BaseAddress { get; set; } = "https://farmbotapi.azurewebsites.net/api/";

        public static string Parameters { get; } = "parameters";

        public static string Plant { get; } = "plant";

        public static string FarmBot { get; } = "farmbot";

        public static string FarmBotPlants { get; } = "farmbotplants";

        public static string Event { get; } = "event";
    }
}
