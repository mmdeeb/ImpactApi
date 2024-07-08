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
    public class ReceiptToRestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceiptToRestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptToRestaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptToRestaurant>>> GetreceiptToRestaurants()
        {
            return await _context.receiptToRestaurants.ToListAsync();
        }

        // GET: api/ReceiptToRestaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptToRestaurant>> GetReceiptToRestaurant(int id)
        {
            var receiptToRestaurant = await _context.receiptToRestaurants.FindAsync(id);

            if (receiptToRestaurant == null)
            {
                return NotFound();
            }

            return receiptToRestaurant;
        }

        // PUT: api/ReceiptToRestaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptToRestaurant(int id, ReceiptToRestaurant receiptToRestaurant)
        {
            if (id != receiptToRestaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptToRestaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptToRestaurantExists(id))
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

        // POST: api/ReceiptToRestaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceiptToRestaurant>> PostReceiptToRestaurant(ReceiptToRestaurant receiptToRestaurant)
        {
            _context.receiptToRestaurants.Add(receiptToRestaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiptToRestaurant", new { id = receiptToRestaurant.Id }, receiptToRestaurant);
        }

        // DELETE: api/ReceiptToRestaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiptToRestaurant(int id)
        {
            var receiptToRestaurant = await _context.receiptToRestaurants.FindAsync(id);
            if (receiptToRestaurant == null)
            {
                return NotFound();
            }

            _context.receiptToRestaurants.Remove(receiptToRestaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptToRestaurantExists(int id)
        {
            return _context.receiptToRestaurants.Any(e => e.Id == id);
        }
    }
}
