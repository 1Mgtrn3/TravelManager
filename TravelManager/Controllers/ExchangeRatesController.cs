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
    public class ExchangeRatesController : ControllerBase
    {
        private readonly TravelManagerContext _context;

        public ExchangeRatesController(TravelManagerContext context)
        {
            _context = context;
        }

        // GET: api/ExchangeRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeRate>>> GetExchangeRates()
        {
            if (_context.ExchangeRates.Count() == 0)
            {
                // Create a new ExRate if collection is empty,

                _context.ExchangeRates.Add(new ExchangeRate {  });
                _context.SaveChanges();
            }
            return await _context.ExchangeRates.ToListAsync();
        }

        // GET: api/ExchangeRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeRate>> GetExchangeRate(long id)
        {
            var exchangeRate = await _context.ExchangeRates.FindAsync(id);

            if (exchangeRate == null)
            {
                return NotFound();
            }

            return exchangeRate;
        }

        // PUT: api/ExchangeRates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExchangeRate(long id, ExchangeRate exchangeRate)
        {
            if (id != exchangeRate.ExchangeRateId)
            {
                return BadRequest();
            }

            _context.Entry(exchangeRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExchangeRateExists(id))
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

        // POST: api/ExchangeRates
        [HttpPost]
        public async Task<ActionResult<ExchangeRate>> PostExchangeRate(ExchangeRate exchangeRate)
        {
            _context.ExchangeRates.Add(exchangeRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExchangeRate", new { id = exchangeRate.ExchangeRateId }, exchangeRate);
        }

        // DELETE: api/ExchangeRates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExchangeRate>> DeleteExchangeRate(long id)
        {
            var exchangeRate = await _context.ExchangeRates.FindAsync(id);
            if (exchangeRate == null)
            {
                return NotFound();
            }

            _context.ExchangeRates.Remove(exchangeRate);
            await _context.SaveChangesAsync();

            return exchangeRate;
        }

        private bool ExchangeRateExists(long id)
        {
            return _context.ExchangeRates.Any(e => e.ExchangeRateId == id);
        }
    }
}
