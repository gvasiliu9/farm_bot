using Entites;
using Services;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tests.Services;
using Xunit;

namespace Tests.Services
{
    public class Tests
    {
        private async Task Add()
        {
            // FarmBot
            var farmBot = new FarmBot
            {
                Id = Globals.FarmBotId,
                Name = "Utm FarmBot",
                IpAddress = "192.168.1.1"
            };

            await Globals.FarmBotService.AddAsync(farmBot);

            // Plant
            var plant = new Plant
            {
                Id = Globals.PlantId,
                Name = "Carot",
                Info = "Info",
                RowDistance = 25,
                SeedDepth = 15,
                WaterQuanity = 250,
                IrigationsPerDay = 5,
                Duration = 30
            };

            await Globals.PlantService.AddAsync(plant);

            // Settings
            var settings = new Settings
            {
                UserId = Globals.UserId,
                FarmBotId = Globals.FarmBotId,
                PlantId = Globals.PlantId,
            };

            await Globals.SettingsService.AddAsync(settings);

            // Parameters
            var parameters = new Parameters
            {
                Id = Globals.ParametersId,
                FarmBotId = Globals.FarmBotId,
                AirTemperature = 15,
                Luminosity = 12,
                SoilHumidity = 15,
            };

            await Globals.ParametersService.AddAsync(parameters);

            // Event
            var farmBotEvent = new Event
            {
                Id = Globals.EventId,
                Type = Enums.EventType.Irigation,
                Info = ""
            };

            await Globals.EventService.AddAsync(farmBotEvent);
        }

        private async Task Get()
        {
            // Plants
            IEnumerable<Plant> plants = await Globals.PlantService.GetAllAsync();

            Assert.NotEmpty(plants);

            // Plant by id
            Plant plant = await Globals.PlantService.GetByIdAsync(Globals.PlantId);

            Assert.NotNull(plant);

            // User settings
            Settings settings = await Globals.SettingsService.GetByIdAsync(Globals.UserId);

            Assert.NotNull(settings);

            // All events
            IEnumerable<Event> events = await Globals.EventService.GetAllAsync();

            Assert.NotEmpty(events);

            // All farmbots
            IEnumerable<FarmBot> farmBots = await Globals.FarmBotService.GetAllAsync();

            Assert.NotEmpty(farmBots);

            // All paramters
            IEnumerable<Parameters> allParameters = await Globals.ParametersService.GetAllAsync();

            Assert.NotEmpty(allParameters);

            // FarmBot parameters
            Parameters parameters = await Globals.ParametersService.GetByIdAsync(Globals.FarmBotId);

            Assert.NotNull(parameters);

            // All settings
            IEnumerable<Settings> allSettings = await Globals.SettingsService.GetAllAsync();

            Assert.NotEmpty(allSettings);
        }

        private async Task Update()
        {
            // Plant
            Plant plant = await Globals.PlantService.GetByIdAsync(Globals.PlantId);

            Assert.NotNull(plant);

            await Globals.PlantService.UpdateAsync(plant);

            // User settings
            Settings settings = await Globals.SettingsService.GetByIdAsync(Globals.UserId);

            Assert.NotNull(settings);

            await Globals.SettingsService.UpdateAsync(settings);

            // FarmBot
            FarmBot farmBot = await Globals.FarmBotService.GetByIdAsync(Globals.FarmBotId);

            Assert.NotNull(farmBot);

            await Globals.FarmBotService.UpdateAsync(farmBot);
        }

        private async Task Delete()
        {
            // Event
            await Globals.EventService.DeleteAsync(Globals.EventId);

            // User settings
            await Globals.SettingsService.DeleteAsync(Globals.UserId);

            // FarmBot parameters
            await Globals.ParametersService.DeleteAsync(Globals.FarmBotId);

            // FarmBot
            await Globals.FarmBotService.DeleteAsync(Globals.FarmBotId);

            // Plant
            await Globals.PlantService.DeleteAsync(Globals.PlantId);
        }

        [Fact]
        public async Task Add_Get_Update_Delete_Success()
        {
            // Add
            await Add();

            // Get
            await Get();

            // Update
            await Update();

            // Delete
            await Delete();
        }
    }
}
