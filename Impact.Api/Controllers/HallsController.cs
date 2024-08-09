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
    public class HallsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Halls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallDTO>>> GetHalls()
        {
            var halls = await _context.halls.ToListAsync();

            var hallDtos = halls.Select(hall => new HallDTO
            {
                Id = hall.Id,
                HallName = hall.HallName,
                ImgLink = hall.ImgLink,
                CenterId = hall.CenterId,
                ListDetials = hall.ListDetials
            }).ToList();

            return Ok(hallDtos);
        }

        // GET: api/Halls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HallDTO>> GetHall(int id)
        {
            var hall = await _context.halls.FindAsync(id);

            if (hall == null)
            {
                return NotFound();
            }

            var hallDto = new HallDTO
            {
                Id = hall.Id,
                HallName = hall.HallName,
                ImgLink = hall.ImgLink,
                CenterId = hall.CenterId,
                ListDetials = hall.ListDetials
            };

            return Ok(hallDto);
        }

        // GET: api/Halls/ByCenter/5
        [HttpGet("ByCenter/{centerId}")]
        public async Task<ActionResult<IEnumerable<HallDTO>>> GetHallsByCenter(int centerId)
        {
            var halls = await _context.halls
                                      .Where(h => h.CenterId == centerId)
                                      .ToListAsync();

            if (halls == null || !halls.Any())
            {
                return NotFound();
            }

            var hallDtos = halls.Select(hall => new HallDTO
            {
                Id = hall.Id,
                HallName = hall.HallName,
                ImgLink = hall.ImgLink,
                CenterId = hall.CenterId,
                ListDetials = hall.ListDetials
            }).ToList();

            return Ok(hallDtos);
        }

        // PUT: api/Halls/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutHall(int id, HallDTO hallDto)
        {
            if (id != hallDto.Id)
            {
                return BadRequest();
            }

            var hall = await _context.halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            hall.HallName = hallDto.HallName;
            hall.ImgLink = hallDto.ImgLink;
            hall.CenterId = hallDto.CenterId;
            hall.ListDetials = hallDto.ListDetials;

            _context.Entry(hall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(id))
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

        // POST: api/Halls
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HallDTO>> PostHall(HallDTO hallDto)
        {
            var hall = new Hall
            {
                HallName = hallDto.HallName,
                ImgLink = hallDto.ImgLink,

                CenterId = hallDto.CenterId,
                ListDetials = hallDto.ListDetials
            };

            _context.halls.Add(hall);
            await _context.SaveChangesAsync();

            hallDto.Id = hall.Id;

            return CreatedAtAction("GetHall", new { id = hall.Id }, hallDto);
        }

        // DELETE: api/Halls/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var hall = await _context.halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            _context.halls.Remove(hall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HallExists(int id)
        {
            return _context.halls.Any(e => e.Id == id);
        }
    }
}
