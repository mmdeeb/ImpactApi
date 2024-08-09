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
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants()
        {
            var restaurants = await _context.restaurants.ToListAsync();

            var restaurantDtos = restaurants.Select(restaurant => new RestaurantDTO
            {
                Id = restaurant.Id,
                RestaurantName = restaurant.RestaurantName,
                PhoneNumber = restaurant.PhoneNumber,
                RestaurantAccountId = restaurant.RestaurantAccountId
            }).ToList();

            return Ok(restaurantDtos);
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant(int id)
        {
            var restaurant = await _context.restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantDto = new RestaurantDTO
            {
                Id = restaurant.Id,
                RestaurantName = restaurant.RestaurantName,
                PhoneNumber = restaurant.PhoneNumber,
                RestaurantAccountId = restaurant.RestaurantAccountId
            };

            return Ok(restaurantDto);
        }

        // PUT: api/Restaurants/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantDTO restaurantDto)
        {
            if (id != restaurantDto.Id)
            {
                return BadRequest();
            }

            var restaurant = await _context.restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            restaurant.RestaurantName = restaurantDto.RestaurantName;
            restaurant.PhoneNumber = restaurantDto.PhoneNumber;
            restaurant.RestaurantAccountId = restaurantDto.RestaurantAccountId;

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Restaurants
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RestaurantDTO>> PostRestaurant(RestaurantDTO restaurantDto)
        {
          
            var restaurantAccount = new RestaurantAccount
            {
                TotalBalance = 0,
                Debt = 0
            };
            
            _context.restaurantAccounts.Add(restaurantAccount);
            await _context.SaveChangesAsync();

            var restaurant = new Restaurant
            {
                RestaurantName = restaurantDto.RestaurantName,
                PhoneNumber = restaurantDto.PhoneNumber,
                RestaurantAccountId = restaurantAccount.Id
            };
           
            _context.restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            restaurantDto.Id = restaurant.Id;

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurantDto);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.restaurants.Any(e => e.Id == id);
        }
    }
}
