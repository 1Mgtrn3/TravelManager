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
    public class DocumentAsigneesController : ControllerBase
    {
        private readonly TravelManagerContext _context;

        public DocumentAsigneesController(TravelManagerContext context)
        {
            _context = context;
        }

        // GET: api/DocumentAsignees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentAsignee>>> GetDocumentAsignees()
        {
            return await _context.DocumentAsignees.ToListAsync();
        }

        // GET: api/DocumentAsignees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentAsignee>> GetDocumentAsignee(long id)
        {
            var documentAsignee = await _context.DocumentAsignees.FindAsync(id);

            if (documentAsignee == null)
            {
                return NotFound();
            }

            return documentAsignee;
        }

        // PUT: api/DocumentAsignees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentAsignee(long id, DocumentAsignee documentAsignee)
        {
            if (id != documentAsignee.DocumentAsigneeId)
            {
                return BadRequest();
            }

            _context.Entry(documentAsignee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentAsigneeExists(id))
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

        // POST: api/DocumentAsignees
        [HttpPost]
        public async Task<ActionResult<DocumentAsignee>> PostDocumentAsignee(DocumentAsignee documentAsignee)
        {
            _context.DocumentAsignees.Add(documentAsignee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumentAsignee", new { id = documentAsignee.DocumentAsigneeId }, documentAsignee);
        }

        // DELETE: api/DocumentAsignees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentAsignee>> DeleteDocumentAsignee(long id)
        {
            var documentAsignee = await _context.DocumentAsignees.FindAsync(id);
            if (documentAsignee == null)
            {
                return NotFound();
            }

            _context.DocumentAsignees.Remove(documentAsignee);
            await _context.SaveChangesAsync();

            return documentAsignee;
        }

        private bool DocumentAsigneeExists(long id)
        {
            return _context.DocumentAsignees.Any(e => e.DocumentAsigneeId == id);
        }
    }
}
