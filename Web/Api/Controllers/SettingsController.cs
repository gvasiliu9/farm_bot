using Data;
using Data.Services;
using Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly FarmBotDbContext _farmBotDbContext;

        private readonly IRepository<Settings> _settingsRepository;

        public SettingsController(FarmBotDbContext farmBotDbContext)
        {
            _farmBotDbContext = farmBotDbContext;

            _settingsRepository = new Repository<Settings>(_farmBotDbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Settings>> Get()
        {
            return _settingsRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Settings> Get(Guid id)
        {
            // Get settings
            Settings settings = _settingsRepository.FirstOrDefault(s => s.UserId == id);

            // Check
            if (settings == null)
                return NotFound();

            // Return result
            return settings;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Settings> Post([FromBody] Settings settings)
        {
            try
            {
                // Check model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Add
                _settingsRepository.Add(settings);

                // Save 
                _farmBotDbContext.SaveChanges();

                // Return result
                return CreatedAtAction(nameof(Get), new { id = settings.UserId }, settings);
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put([FromBody] Settings settings)
        {
            try
            {
                // Check model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Get settings
                Settings settingsToUpdate = _settingsRepository.
                    FirstOrDefault(s => s.UserId == settings.UserId);

                // Check result
                if (settingsToUpdate == null)
                    return NotFound();

                // Update
                settingsToUpdate.FarmBotId = settings.FarmBotId;
                settingsToUpdate.PlantId = settings.PlantId;
                settingsToUpdate.Updated = DateTime.Now;

                // Save
                _farmBotDbContext.SaveChanges();

                // Return result
                return NoContent();
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{userId}")]
        public ActionResult Delete(Guid userId)
        {
            try
            {
                // Get settings
                Settings settingsToRemove = _settingsRepository.FirstOrDefault(s => s.UserId == userId);

                // Check
                if (settingsToRemove == null)
                    return NotFound();

                // Remove
                _settingsRepository.Remove(settingsToRemove);

                // Save
                _farmBotDbContext.SaveChanges();

                // Return result
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
