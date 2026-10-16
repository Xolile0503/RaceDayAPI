
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public EventsController(RaceDayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.RaceEvents
                .Select(e => new
                {
                    e.RaceEventId,
                    e.Name,
                    e.Description,
                    e.Location,
                    e.EventDate,
                    e.OrganiserId
                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var raceEvent = await _context.RaceEvents
                .Where(e => e.RaceEventId == id)
                .Select(e => new
                {
                    e.RaceEventId,
                    e.Name,
                    e.Description,
                    e.Location,
                    e.EventDate,
                    e.OrganiserId
                })
                .SingleOrDefaultAsync();

            if (raceEvent == null)
                return NotFound("Race event not found.");

            return Ok(raceEvent);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(
            RaceEvent raceEvent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool organiserExists = await _context.Organisers
                .AnyAsync(o => o.OrganiserId == raceEvent.OrganiserId);

            if (!organiserExists)
                return BadRequest("Organiser does not exist.");

            if (raceEvent.EventDate <= DateTime.UtcNow)
                return BadRequest("Event date must be in the future.");

            raceEvent.RaceEventId = 0;
            raceEvent.Organiser = null;

            _context.RaceEvents.Add(raceEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEvent),
                new { id = raceEvent.RaceEventId },
                raceEvent);
        }
    }
}
