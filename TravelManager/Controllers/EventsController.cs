using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManager.Helpers;
using TravelManager.Models;

namespace TravelManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly TravelManagerContext _context;
        private readonly UserManager<UserIdentity> _userManager;

        public EventsController(UserManager<UserIdentity> userManager, TravelManagerContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/Events
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var listToConvert =  await _context.Events.ToListAsync();
            foreach (var item in listToConvert)
            {
               await AdjustToUser(item);
            }
            return listToConvert;
        }

        // GET: api/Events/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(long id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return await AdjustToUser(@event);
        }

        // PUT: api/Events/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(long id, Event @event)
        {
            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }

        // DELETE: api/Events/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(long id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(long id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }

        private async Task<Event> AdjustToUser(Event @event) {
            UserIdentity identityUser = await GetCurrentUserAsync();
            var identityUserId = identityUser?.Id;
            User user = await _context.Users.SingleOrDefaultAsync(u => u.IdentityId == identityUserId);

            if (@event.CurrencyId != user.CurrencyId)
            {
                var converter = new CurrencyConverter(_context);
                var convertedCost = converter.Convert(@event.Cost, @event.CurrencyId, user.CurrencyId);
                if (convertedCost != @event.Cost)
                {

                    @event.CurrencyId = user.CurrencyId;
                    @event.Cost = convertedCost;

                }
                
            }
            return @event;

        }
        private Task<UserIdentity> GetCurrentUserAsync() =>  _userManager.FindByNameAsync(HttpContext.User.Claims.ToAsyncEnumerable().ElementAt(0).Result.Value);
    }
}
