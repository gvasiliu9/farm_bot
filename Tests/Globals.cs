using Services;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Services
{
    public static class Globals
    {
        public static Guid FarmBotId { get; } = Guid.Parse("aeec0d32-14ab-44de-97f5-cd83bcf5cdaf");

        public static Guid PlantId { get; } = Guid.Parse("cabd4045-2c5f-4000-906e-7521394d8286");

        public static Guid UserId { get; } = Guid.Parse("47fda420-bf99-4a16-9f50-3bff38bc94c4");

        public static Guid EventId { get; } = Guid.Parse("aab5c88d-ea29-48d7-a66a-ef2d8ee203fb");

        public static Guid ParametersId { get; } = Guid.Parse("e5f49e7e-c0b4-4161-bc09-8ec84121e3ef");

        public static IEventService EventService { get; } 
            = new EventService(new HttpRequest());

        public static IFarmBotService FarmBotService { get; } 
            = new FarmBotService(new HttpRequest());

        public static IParametersService ParametersService { get; }
            = new ParametersService(new HttpRequest());

        public static IPlantService PlantService { get; } 
            = new PlantService(new HttpRequest());

        public static ISettingsService SettingsService { get; } 
            = new SettingsService(new HttpRequest());
    }
}
