using Data;
using Data.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmBotPlantsController : ControllerBase
    {
        private readonly FarmBotDbContext _farmBotDbContext;

        private readonly IRepository<FarmBotPlant> _farmBotPlantsRepository;

        public FarmBotPlantsController(FarmBotDbContext farmBotDbContext)
        {
            _farmBotDbContext = farmBotDbContext;

            _farmBotPlantsRepository = new Repository<FarmBotPlant>(_farmBotDbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<FarmBotPlant>> Get()
        {
            // Return result
            return _farmBotPlantsRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<FarmBotPlant>> GetById(Guid id)
        {
            // Return result
            return _farmBotPlantsRepository.Where(f => f.FarmBotId == id).ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Event> Post([FromBody] FarmBotPlant farmBotPlant)
        {
            try
            {
                // Check model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Add
                _farmBotPlantsRepository.Add(farmBotPlant);

                // Save
                _farmBotDbContext.SaveChanges();

                // Return result
                return CreatedAtAction(nameof(Get), farmBotPlant);
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
                // Get farmbot plant
                FarmBotPlant farmBotPlantToRemove = _farmBotPlantsRepository
                    .FirstOrDefault(e => e.PlantId == id);

                // Check
                if (farmBotPlantToRemove == null)
                    return NotFound();

                // Remove
                _farmBotPlantsRepository.Remove(farmBotPlantToRemove);

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
                // Get event
                IQueryable<FarmBotPlant> farmBotPlantsToRemove = _farmBotPlantsRepository.GetAll();

                // Check
                if (farmBotPlantsToRemove.Any())
                    return NotFound();

                // Remove
                _farmBotPlantsRepository.RemoveRange(farmBotPlantsToRemove);

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
