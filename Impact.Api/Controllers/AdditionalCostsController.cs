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
    public class AdditionalCostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdditionalCostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdditionalCosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalCost>>> GetadditionalCosts()
        {
            return await _context.additionalCosts.ToListAsync();
        }

        // GET: api/AdditionalCosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalCost>> GetAdditionalCost(int id)
        {
            var additionalCost = await _context.additionalCosts.FindAsync(id);

            if (additionalCost == null)
            {
                return NotFound();
            }

            return additionalCost;
        }

        // PUT: api/AdditionalCosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalCost(int id, AdditionalCost additionalCost)
        {
            if (id != additionalCost.Id)
            {
                return BadRequest();
            }

            _context.Entry(additionalCost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalCostExists(id))
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

        // POST: api/AdditionalCosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdditionalCost>> PostAdditionalCost(AdditionalCost additionalCost)
        {
            _context.additionalCosts.Add(additionalCost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalCost", new { id = additionalCost.Id }, additionalCost);
        }

        // DELETE: api/AdditionalCosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalCost(int id)
        {
            var additionalCost = await _context.additionalCosts.FindAsync(id);
            if (additionalCost == null)
            {
                return NotFound();
            }

            _context.additionalCosts.Remove(additionalCost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdditionalCostExists(int id)
        {
            return _context.additionalCosts.Any(e => e.Id == id);
        }
    }
}
