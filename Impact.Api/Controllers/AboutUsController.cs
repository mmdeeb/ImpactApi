using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ImpactBackend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AboutUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AboutUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AboutUs
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AboutUs>>> GetaboutUs()
        {
            return await _context.aboutUs.ToListAsync();
        }

        // GET: api/AboutUs/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AboutUs>> GetAboutUs(int id)
        {
            var aboutUs = await _context.aboutUs.FindAsync(id);

            if (aboutUs == null)
            {
                return NotFound();
            }

            return aboutUs;
        }

        // PUT: api/AboutUs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAboutUs(int id, AboutUs aboutUs)
        {
            if (id != aboutUs.Id)
            {
                return BadRequest();
            }

            _context.Entry(aboutUs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutUsExists(id))
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

        // POST: api/AboutUs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AboutUs>> PostAboutUs(AboutUs aboutUs)
        {
            _context.aboutUs.Add(aboutUs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAboutUs", new { id = aboutUs.Id }, aboutUs);
        }

        // DELETE: api/AboutUs/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAboutUs(int id)
        {
            var aboutUs = await _context.aboutUs.FindAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            _context.aboutUs.Remove(aboutUs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AboutUsExists(int id)
        {
            return _context.aboutUs.Any(e => e.Id == id);
        }
    }
}
