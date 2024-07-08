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
    public class CentersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CentersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Centers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Center>>> Getcenters()
        {
            return await _context.centers.ToListAsync();
        }

        // GET: api/Centers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Center>> GetCenter(int id)
        {
            var center = await _context.centers.FindAsync(id);

            if (center == null)
            {
                return NotFound();
            }

            return center;
        }

        // PUT: api/Centers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCenter(int id, Center center)
        {
            if (id != center.Id)
            {
                return BadRequest();
            }

            _context.Entry(center).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CenterExists(id))
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

        // POST: api/Centers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Center>> PostCenter(Center center)
        {
            _context.centers.Add(center);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCenter", new { id = center.Id }, center);
        }

        // DELETE: api/Centers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenter(int id)
        {
            var center = await _context.centers.FindAsync(id);
            if (center == null)
            {
                return NotFound();
            }

            _context.centers.Remove(center);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CenterExists(int id)
        {
            return _context.centers.Any(e => e.Id == id);
        }
    }
}
