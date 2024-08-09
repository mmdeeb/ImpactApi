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
    public class RestaurantAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantAccounts
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RestaurantAccountDTO>>> GetRestaurantAccounts()
        {
            var restaurantAccounts = await _context.restaurantAccounts.ToListAsync();

            var restaurantAccountDtos = restaurantAccounts.Select(account => new RestaurantAccountDTO
            {
                Id = account.Id,
                TotalBalance = account.TotalBalance,
                Debt = account.Debt,
                RestaurantId = account.Restaurant.Id
            }).ToList();

            return Ok(restaurantAccountDtos);
        }

        // GET: api/RestaurantAccounts/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<RestaurantAccountDTO>> GetRestaurantAccount(int id)
        {
            var restaurantAccount = await _context.restaurantAccounts.FindAsync(id);

            if (restaurantAccount == null)
            {
                return NotFound();
            }

            var restaurantAccountDto = new RestaurantAccountDTO
            {
                Id = restaurantAccount.Id,
                TotalBalance = restaurantAccount.TotalBalance,
                Debt = restaurantAccount.Debt,
                RestaurantId = restaurantAccount.Restaurant.Id
            };

            return Ok(restaurantAccountDto);
        }

        // PUT: api/RestaurantAccounts/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRestaurantAccount(int id, RestaurantAccountDTO restaurantAccountDto)
        {
            if (id != restaurantAccountDto.Id)
            {
                return BadRequest();
            }

            var restaurantAccount = await _context.restaurantAccounts.FindAsync(id);
            if (restaurantAccount == null)
            {
                return NotFound();
            }

            restaurantAccount.TotalBalance = restaurantAccountDto.TotalBalance;
            restaurantAccount.Debt = restaurantAccountDto.Debt;

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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RestaurantAccountDTO>> PostRestaurantAccount(RestaurantAccountDTO restaurantAccountDto)
        {
            var restaurantAccount = new RestaurantAccount
            {
                TotalBalance = restaurantAccountDto.TotalBalance,
                Debt = restaurantAccountDto.Debt,
            };

            _context.restaurantAccounts.Add(restaurantAccount);
            await _context.SaveChangesAsync();

            restaurantAccountDto.Id = restaurantAccount.Id;

            return CreatedAtAction("GetRestaurantAccount", new { id = restaurantAccount.Id }, restaurantAccountDto);
        }

        // DELETE: api/RestaurantAccounts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
