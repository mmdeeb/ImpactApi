using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ImpactBackend.Infrastructure.Persistence;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OtherExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OtherExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtherExpenses>>> GetotherExpenses()
        {
            return await _context.otherExpenses.ToListAsync();
        }

        // GET: api/OtherExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OtherExpenses>> GetOtherExpenses(int id)
        {
            var otherExpenses = await _context.otherExpenses.FindAsync(id);

            if (otherExpenses == null)
            {
                return NotFound();
            }

            return otherExpenses;
        }

        // PUT: api/OtherExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtherExpenses(int id, OtherExpenses otherExpenses)
        {
            if (id != otherExpenses.Id)
            {
                return BadRequest();
            }

            _context.Entry(otherExpenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtherExpensesExists(id))
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

        // POST: api/OtherExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OtherExpenses>> PostOtherExpenses(OtherExpenses otherExpenses)
        {
            _context.otherExpenses.Add(otherExpenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOtherExpenses", new { id = otherExpenses.Id }, otherExpenses);
        }

        // DELETE: api/OtherExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtherExpenses(int id)
        {
            var otherExpenses = await _context.otherExpenses.FindAsync(id);
            if (otherExpenses == null)
            {
                return NotFound();
            }

            _context.otherExpenses.Remove(otherExpenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OtherExpensesExists(int id)
        {
            return _context.otherExpenses.Any(e => e.Id == id);
        }
    }
}
