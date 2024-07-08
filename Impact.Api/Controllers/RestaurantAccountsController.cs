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
    public class RestaurantAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantAccount>>> GetrestaurantAccounts()
        {
            return await _context.restaurantAccounts.ToListAsync();
        }

        // GET: api/RestaurantAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantAccount>> GetRestaurantAccount(int id)
        {
            var restaurantAccount = await _context.restaurantAccounts.FindAsync(id);

            if (restaurantAccount == null)
            {
                return NotFound();
            }

            return restaurantAccount;
        }

        // PUT: api/RestaurantAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantAccount(int id, RestaurantAccount restaurantAccount)
        {
            if (id != restaurantAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurantAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantAccountExists(id))
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

        // POST: api/RestaurantAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RestaurantAccount>> PostRestaurantAccount(RestaurantAccount restaurantAccount)
        {
            _context.restaurantAccounts.Add(restaurantAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantAccount", new { id = restaurantAccount.Id }, restaurantAccount);
        }

        // DELETE: api/RestaurantAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantAccount(int id)
        {
            var restaurantAccount = await _context.restaurantAccounts.FindAsync(id);
            if (restaurantAccount == null)
            {
                return NotFound();
            }

            _context.restaurantAccounts.Remove(restaurantAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantAccountExists(int id)
        {
            return _context.restaurantAccounts.Any(e => e.Id == id);
        }
    }
}
