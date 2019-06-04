using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private readonly FarmBotDbContext _farmBotDbContext;

        private readonly IRepository<Parameters> _parametersRepository;

        public ParametersController(FarmBotDbContext farmBotDbContext)
        {
            _farmBotDbContext = farmBotDbContext;

            _parametersRepository = new Repository<Parameters>(_farmBotDbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Parameters>> Get()
        {
            // Return result
            return _parametersRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Parameters> Get(Guid id)
        {
            // Get CurrentParameters
            Parameters parameters = _parametersRepository
                .FirstOrDefault(p => p.FarmBotId == id);

            // Check
            if (parameters == null)
                return NotFound();

            // Return result
            return parameters;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Parameters> Post([FromBody] Parameters parameters)
        {
            try
            {
                // Check model
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Add
                _parametersRepository.Add(parameters);

                // Save
                _farmBotDbContext.SaveChanges();

                // Return result
                return CreatedAtAction(nameof(Get), parameters);
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Put([FromBody] Parameters currentParameters)
        {
            try
            {
                // Get CurrentParameters
                Parameters parametersToUpdate = _parametersRepository
                    .FirstOrDefault(p => p.FarmBotId == currentParameters.FarmBotId);

                // Check
                if (parametersToUpdate == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Update
                parametersToUpdate.AirTemperature = currentParameters.AirTemperature;
                parametersToUpdate.FarmBotId = currentParameters.FarmBotId;
                parametersToUpdate.Luminosity = currentParameters.Luminosity;
                parametersToUpdate.SoilHumidity = currentParameters.SoilHumidity;
                parametersToUpdate.SeededPlants = currentParameters.SeededPlants;
                parametersToUpdate.Updated = DateTime.Now;

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

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                // Get CurrentParameters
                Parameters parametersToRemove = 
                    _parametersRepository.FirstOrDefault(p => p.FarmBotId == id);

                // Check
                if (parametersToRemove == null)
                    return NotFound();

                // Remove
                _parametersRepository.Remove(parametersToRemove);

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
