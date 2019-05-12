using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelManager.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : Controller
    {
        private readonly TravelManagerContext _context;
        public CurrenciesController(TravelManagerContext context)
        {
            _context = context;
            if (_context.Currencies.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Currencies.Add(new Currency { Name = "American Dollar (USD)" , Symbol = '$' });
                _context.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/<controller>/id
        [HttpGet("{Id}")]
        public async Task<ActionResult<Currency>> GetCurrency(long Id)
        {
            var currency = await _context.Currencies.FindAsync(Id);

            if(currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurrency), new { Id = currency.CurrencyId }, currency);
        }

        // PUT api/<controller>/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCurrency(long Id, Currency currency)
        {

            if(Id != currency.CurrencyId)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetCurrency), new { Id = currency.CurrencyId }, currency);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCurrency(long Id)
        {
            var currency = await _context.Currencies.FindAsync(Id);

            if(currency == null)
            {
                return NotFound();
            }

            try
            {
                _context.Currencies.Remove(currency);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
            return NoContent();
        }

       
    }
}
