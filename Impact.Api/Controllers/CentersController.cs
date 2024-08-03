using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ImpactApi.Infrastructure.Persistence;
using Impact.Api.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<IEnumerable<CenterDTO>>> GetCenters()
        {
            var centers = await _context.centers.ToListAsync();

            var centerDtos = centers.Select(center => new CenterDTO
            {
                Id = center.Id,
                CenterName = center.CenterName,
                CenterLocation = center.CenterLocation,
                PhoneNumber = center.PhoneNumber,
                Media = center.Media
            }).ToList();

            return Ok(centerDtos);
        }

        // GET: api/Centers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CenterDTO>> GetCenter(int id)
        {
            var center = await _context.centers.FindAsync(id);

            if (center == null)
            {
                return NotFound();
            }

            var centerDto = new CenterDTO
            {
                Id = center.Id,
                CenterName = center.CenterName,
                CenterLocation = center.CenterLocation,
                PhoneNumber = center.PhoneNumber,
                Media = center.Media
            };

            return Ok(centerDto);
        }

        // PUT: api/Centers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCenter(int id, CenterDTO centerDto)
        {
            if (id != centerDto.Id)
            {
                return BadRequest();
            }

            var center = await _context.centers.FindAsync(id);
            if (center == null)
            {
                return NotFound();
            }

            center.CenterName = centerDto.CenterName;
            center.CenterLocation = centerDto.CenterLocation;
            center.PhoneNumber = centerDto.PhoneNumber;
            center.Media = centerDto.Media;

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
        public async Task<ActionResult<CenterDTO>> PostCenter(CenterDTO centerDto)
        {
            var center = new Center
            {
                CenterName = centerDto.CenterName,
                CenterLocation = centerDto.CenterLocation,
                PhoneNumber = centerDto.PhoneNumber,
                Media = centerDto.Media
            };

            _context.centers.Add(center);
            await _context.SaveChangesAsync();

            centerDto.Id = center.Id;

            return CreatedAtAction("GetCenter", new { id = center.Id }, centerDto);
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
