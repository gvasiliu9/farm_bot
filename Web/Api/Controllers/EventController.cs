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
    public class EventController : ControllerBase
    {
        private readonly FarmBotDbContext _farmBotDbContext;

        private readonly IRepository<Event> _eventRepository;

        public EventController(FarmBotDbContext farmBotDbContext)
        {
            _farmBotDbContext = farmBotDbContext;

            _eventRepository = new Repository<Event>(_farmBotDbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Event>> Get()
        {
            // Return result
            return _eventRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Event> Get(Guid id)
        {
            // Get event
            Event farmBotEvent = _eventRepository.FirstOrDefault(p => p.Id == id);

            // Check
            if (farmBotEvent == null)
                return NotFound();

            // Return result
            return farmBotEvent;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Event> Post([FromBody] Event farmBotEvent)
        {
            try
            {
                // Check model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Add
                _eventRepository.Add(farmBotEvent);

                // Save
                _farmBotDbContext.SaveChanges();

                // Return result
                return CreatedAtAction(nameof(Get), new { id = farmBotEvent.Id }, farmBotEvent);
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
                // Get event
                Event eventToRemove = _eventRepository.FirstOrDefault(e => e.Id == e.Id);

                // Check
                if (eventToRemove == null)
                    return NotFound();

                // Remove
                _eventRepository.Remove(eventToRemove);

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
