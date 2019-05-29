using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.SignalR;
using Data;
using Data.Services;
using Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        #region Services

        private readonly FarmBotDbContext _farmBotDbContext;

        private readonly IRepository<Plant> _plantRepository;

        private readonly IHubContext<CommunicationHub, ICommunicationHub> _communicationHubContext;

        #endregion

        public PlantController(FarmBotDbContext farmBotDbContext
            , IHubContext<CommunicationHub, ICommunicationHub> communicationHubContext)
        {
            _farmBotDbContext = farmBotDbContext;

            _plantRepository = new Repository<Plant>(_farmBotDbContext);

            _communicationHubContext = communicationHubContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Plant>> Get()
        {
            // Return result
            return _plantRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Plant> Get(Guid id)
        {
            // Get plant
            Plant plant = _plantRepository.FirstOrDefault(p => p.Id == id);

            // Check
            if (plant == null)
                return NotFound();

            // Return result
            return plant;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Plant>> Post([FromBody] Plant plant)
        {
            try
            {
                // Check model
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Add
                _plantRepository.Add(plant);

                // Save
                _farmBotDbContext.SaveChanges();

                // Notify
                await _communicationHubContext.Clients.All.ReceiveMessage("", $"{plant.Name} plant created");

                // Return result
                return CreatedAtAction(nameof(Get), plant);
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Put([FromBody] Plant plant)
        {
            try
            {
                // Get plant
                Plant plantToUpdate = _plantRepository.GetById(plant.Id);

                // Check
                if (plantToUpdate == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Update
                plantToUpdate.Name = plant.Name;
                plantToUpdate.Info = plant.Info;
                plantToUpdate.RowDistance = plant.RowDistance;
                plantToUpdate.SeedDepth = plant.SeedDepth;
                plantToUpdate.WaterQuanity = plant.WaterQuanity;
                plantToUpdate.IrigationsPerDay = plant.IrigationsPerDay;
                plantToUpdate.Duration = plant.Duration;
                plantToUpdate.Updated = DateTime.Now;

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
                // Get plant
                Plant plantToRemove = _plantRepository.GetById(id);

                // Check
                if (plantToRemove == null)
                    return NotFound();

                // Remove
                _plantRepository.Remove(plantToRemove);

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

        [HttpDelete]
        public ActionResult Delete()
        {
            try
            {
                // Get plant
                IQueryable<Plant> plantsToRemove = _plantRepository.GetAll();

                // Check
                if (!plantsToRemove.Any())
                    return NotFound();

                // Remove
                _plantRepository.RemoveRange(plantsToRemove);

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
