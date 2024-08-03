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
    public class ReceiptsToRestaurantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceiptsToRestaurantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptsToRestaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptToRestaurantDTO>>> GetReceiptsToRestaurant()
        {
            var receipts = await _context.receiptsToRestaurant.ToListAsync();

            var receiptDtos = receipts.Select(receipt => new ReceiptToRestaurantDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                RestaurantAccountId = receipt.RestaurantAccountId
            }).ToList();

            return Ok(receiptDtos);
        }

        // GET: api/ReceiptsToRestaurant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptToRestaurantDTO>> GetReceiptToRestaurant(int id)
        {
            var receipt = await _context.receiptsToRestaurant.FindAsync(id);

            if (receipt == null)
            {
                return NotFound();
            }

            var receiptDto = new ReceiptToRestaurantDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                RestaurantAccountId = receipt.RestaurantAccountId
            };

            return Ok(receiptDto);
        }

        // GET: api/ReceiptsToRestaurant/ByRestaurantAccount/5
        [HttpGet("ByRestaurantAccount/{restaurantAccountId}")]
        public async Task<ActionResult<IEnumerable<ReceiptToRestaurantDTO>>> GetReceiptsByRestaurantAccount(int restaurantAccountId)
        {
            var receipts = await _context.receiptsToRestaurant
                                         .Where(r => r.RestaurantAccountId == restaurantAccountId)
                                         .ToListAsync();

            if (!receipts.Any())
            {
                return NotFound();
            }

            var receiptDtos = receipts.Select(receipt => new ReceiptToRestaurantDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                RestaurantAccountId = receipt.RestaurantAccountId
            }).ToList();

            return Ok(receiptDtos);
        }

        // PUT: api/ReceiptsToRestaurant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptToRestaurant(int id, ReceiptToRestaurantDTO receiptDto)
        {
            if (id != receiptDto.Id)
            {
                return BadRequest();
            }

            var receipt = await _context.receiptsToRestaurant.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            var previousAmount = receipt.Amount;

            receipt.Date = receiptDto.Date;
            receipt.Receiver = receiptDto.Receiver;
            receipt.Payer = receiptDto.Payer;
            receipt.Amount = receiptDto.Amount;
            receipt.RestaurantAccountId = receiptDto.RestaurantAccountId;

            _context.Entry(receipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var restaurantAccount = await _context.restaurantAccounts.FindAsync(receipt.RestaurantAccountId);
                if (restaurantAccount != null)
                {
                    restaurantAccount.Debt += previousAmount; 
                    restaurantAccount.Debt -= receipt.Amount;
                    _context.Entry(restaurantAccount).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
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

        // POST: api/ReceiptsToRestaurant
        [HttpPost]
        public async Task<ActionResult<ReceiptToRestaurantDTO>> PostReceiptToRestaurant(ReceiptToRestaurantDTO receiptDto)
        {
            var receipt = new ReceiptToRestaurant
            {
                Date = receiptDto.Date,
                Receiver = receiptDto.Receiver,
                Payer = receiptDto.Payer,
                Amount = receiptDto.Amount,
                RestaurantAccountId = receiptDto.RestaurantAccountId
            };

            _context.receiptsToRestaurant.Add(receipt);
            await _context.SaveChangesAsync();

            var restaurantAccount = await _context.restaurantAccounts.FindAsync(receipt.RestaurantAccountId);
            if (restaurantAccount != null)
            {
                restaurantAccount.Debt -= receipt.Amount;
                _context.Entry(restaurantAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            receiptDto.Id = receipt.Id;

            return CreatedAtAction("GetReceiptToRestaurant", new { id = receipt.Id }, receiptDto);
        }

        // DELETE: api/ReceiptsToRestaurant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiptToRestaurant(int id)
        {
            var receipt = await _context.receiptsToRestaurant.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            var amount = receipt.Amount;

            _context.receiptsToRestaurant.Remove(receipt);
            await _context.SaveChangesAsync();

            var restaurantAccount = await _context.restaurantAccounts.FindAsync(receipt.RestaurantAccountId);
            if (restaurantAccount != null)
            {
                restaurantAccount.Debt += amount;
                _context.Entry(restaurantAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool ReceiptToRestaurantExists(int id)
        {
            return _context.receiptsToRestaurant.Any(e => e.Id == id);
        }
    }
}
