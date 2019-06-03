using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmBotController : ControllerBase
    {
        private readonly FarmBotDbContext _farmBotDbContext;

        private readonly IRepository<FarmBot> _farmBotRepository;

        public FarmBotController(FarmBotDbContext farmBotDbContext)
        {
            _farmBotDbContext = farmBotDbContext;

            _farmBotRepository = new Repository<FarmBot>(_farmBotDbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<FarmBot>> Get()
        {
            // Return result
            return _farmBotRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<FarmBot> Get(Guid id)
        {
            // Get FarmBot
            FarmBot FarmBot = _farmBotRepository.FirstOrDefault(p => p.Id == id);

            // Check
            if (FarmBot == null)
                return NotFound();

            // Return result
            return FarmBot;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FarmBot> Post([FromBody] FarmBot FarmBot)
        {
            try
            {
                // Check model
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Add
                _farmBotRepository.Add(FarmBot);

                // Save
                _farmBotDbContext.SaveChanges();

                // Return result
                return CreatedAtAction(nameof(Get), FarmBot);
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Put([FromBody] FarmBot farmBot)
        {
            try
            {
                // Get FarmBot
                FarmBot farmBotToUpdate = _farmBotRepository.GetById(farmBot.Id);

                // Check
                if (farmBotToUpdate == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Update
                farmBotToUpdate.IpAddress = farmBot.IpAddress;
                farmBotToUpdate.Name = farmBot.Name;
                farmBotToUpdate.IpCameraAddress = farmBot.IpCameraAddress;
                farmBotToUpdate.LastX = farmBot.LastX;
                farmBotToUpdate.LastY = farmBot.LastY;
                farmBotToUpdate.Width = farmBot.Width;
                farmBotToUpdate.Length = farmBot.Length;
                farmBotToUpdate.Updated = DateTime.Now;

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
                // Get FarmBot
                FarmBot farmBotToRemove = _farmBotRepository.GetById(id);

                // Check
                if (farmBotToRemove == null)
                    return NotFound();

                // Remove
                _farmBotRepository.Remove(farmBotToRemove);

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
