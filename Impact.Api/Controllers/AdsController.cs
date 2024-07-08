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
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ads>>> Getads()
        {
            return await _context.ads.ToListAsync();
        }

        // GET: api/Ads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ads>> GetAds(int id)
        {
            var ads = await _context.ads.FindAsync(id);

            if (ads == null)
            {
                return NotFound();
            }

            return ads;
        }

        // PUT: api/Ads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAds(int id, Ads ads)
        {
            if (id != ads.Id)
            {
                return BadRequest();
            }

            _context.Entry(ads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdsExists(id))
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

        // POST: api/Ads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ads>> PostAds(Ads ads)
        {
            _context.ads.Add(ads);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAds", new { id = ads.Id }, ads);
        }

        // DELETE: api/Ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAds(int id)
        {
            var ads = await _context.ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }

            _context.ads.Remove(ads);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdsExists(int id)
        {
            return _context.ads.Any(e => e.Id == id);
        }
    }
}
