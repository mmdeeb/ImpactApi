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
    public class ReceiptsFromClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceiptsFromClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptsFromClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptFromClientDTO>>> GetReceiptsFromClient()
        {
            var receipts = await _context.receiptsFromClient.ToListAsync();

            var receiptDtos = receipts.Select(receipt => new ReceiptFromClientDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                ClientAccountId = receipt.ClientAccountId
            }).ToList();

            return Ok(receiptDtos);
        }

        // GET: api/ReceiptsFromClient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptFromClientDTO>> GetReceiptFromClient(int id)
        {
            var receipt = await _context.receiptsFromClient.FindAsync(id);

            if (receipt == null)
            {
                return NotFound();
            }

            var receiptDto = new ReceiptFromClientDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                ClientAccountId = receipt.ClientAccountId
            };

            return Ok(receiptDto);
        }

        // GET: api/ReceiptsFromClient/ByClientAccount/5
        [HttpGet("ByClientAccount/{clientAccountId}")]
        public async Task<ActionResult<IEnumerable<ReceiptFromClientDTO>>> GetReceiptsByClientAccount(int clientAccountId)
        {
            var receipts = await _context.receiptsFromClient
                                         .Where(r => r.ClientAccountId == clientAccountId)
                                         .ToListAsync();

            if (!receipts.Any())
            {
                return NotFound();
            }

            var receiptDtos = receipts.Select(receipt => new ReceiptFromClientDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                ClientAccountId = receipt.ClientAccountId
            }).ToList();

            return Ok(receiptDtos);
        }

        // PUT: api/ReceiptsFromClient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptFromClient(int id, ReceiptFromClientDTO receiptDto)
        {
            if (id != receiptDto.Id)
            {
                return BadRequest();
            }

            var receipt = await _context.receiptsFromClient.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            var previousAmount = receipt.Amount;

            receipt.Date = receiptDto.Date;
            receipt.Receiver = receiptDto.Receiver;
            receipt.Payer = receiptDto.Payer;
            receipt.Amount = receiptDto.Amount;
            receipt.ClientAccountId = receiptDto.ClientAccountId;

            _context.Entry(receipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var clientAccount = await _context.clientAccounts.FindAsync(receipt.ClientAccountId);
                if (clientAccount != null)
                {
                    clientAccount.Debt += previousAmount;
                    clientAccount.Debt -= receipt.Amount; 
                    _context.Entry(clientAccount).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptFromClientExists(id))
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

        // POST: api/ReceiptsFromClient
        [HttpPost]
        public async Task<ActionResult<ReceiptFromClientDTO>> PostReceiptFromClient(ReceiptFromClientDTO receiptDto)
        {
            var receipt = new ReceiptFromClient
            {
                Date = receiptDto.Date,
                Receiver = receiptDto.Receiver,
                Payer = receiptDto.Payer,
                Amount = receiptDto.Amount,
                ClientAccountId = receiptDto.ClientAccountId
            };

            _context.receiptsFromClient.Add(receipt);
            await _context.SaveChangesAsync();

            var clientAccount = await _context.clientAccounts.FindAsync(receipt.ClientAccountId);
            if (clientAccount != null)
            {
                clientAccount.Debt -= receipt.Amount;
                _context.Entry(clientAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            receiptDto.Id = receipt.Id;

            return CreatedAtAction("GetReceiptFromClient", new { id = receipt.Id }, receiptDto);
        }

        // DELETE: api/ReceiptsFromClient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiptFromClient(int id)
        {
            var receipt = await _context.receiptsFromClient.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            var amount = receipt.Amount;

            _context.receiptsFromClient.Remove(receipt);
            await _context.SaveChangesAsync();

            var clientAccount = await _context.clientAccounts.FindAsync(receipt.ClientAccountId);
            if (clientAccount != null)
            {
                clientAccount.Debt += amount;
                _context.Entry(clientAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool ReceiptFromClientExists(int id)
        {
            return _context.receiptsFromClient.Any(e => e.Id == id);
        }
    }
}
