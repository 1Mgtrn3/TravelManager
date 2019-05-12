using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManager.Models;

namespace TravelManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAsigneesController : ControllerBase
    {
        private readonly TravelManagerContext _context;

        public RoleAsigneesController(TravelManagerContext context)
        {
            _context = context;
        }

        // GET: api/RoleAsignees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleAsignee>>> GetRoleAsignees()
        {
            return await _context.RoleAsignees.ToListAsync();
        }

        // GET: api/RoleAsignees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleAsignee>> GetRoleAsignee(long id)
        {
            var roleAsignee = await _context.RoleAsignees.FindAsync(id);

            if (roleAsignee == null)
            {
                return NotFound();
            }

            return roleAsignee;
        }

        // PUT: api/RoleAsignees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleAsignee(long id, RoleAsignee roleAsignee)
        {
            if (id != roleAsignee.RoleAsigneeId)
            {
                return BadRequest();
            }

            _context.Entry(roleAsignee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleAsigneeExists(id))
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

        // POST: api/RoleAsignees
        [HttpPost]
        public async Task<ActionResult<RoleAsignee>> PostRoleAsignee(RoleAsignee roleAsignee)
        {
            _context.RoleAsignees.Add(roleAsignee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleAsignee", new { id = roleAsignee.RoleAsigneeId }, roleAsignee);
        }

        // DELETE: api/RoleAsignees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoleAsignee>> DeleteRoleAsignee(long id)
        {
            var roleAsignee = await _context.RoleAsignees.FindAsync(id);
            if (roleAsignee == null)
            {
                return NotFound();
            }

            _context.RoleAsignees.Remove(roleAsignee);
            await _context.SaveChangesAsync();

            return roleAsignee;
        }

        private bool RoleAsigneeExists(long id)
        {
            return _context.RoleAsignees.Any(e => e.RoleAsigneeId == id);
        }
    }
}
