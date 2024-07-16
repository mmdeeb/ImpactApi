using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ImpactBackend.Infrastructure.Persistence;
using Impact.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReceiptsToEmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceiptsToEmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptsToEmployee
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReceiptToEmployeeDTO>>> GetReceiptsToEmployee()
        {
            var receipts = await _context.receiptsToEmployee.ToListAsync();

            var receiptDtos = receipts.Select(receipt => new ReceiptToEmployeeDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                EmployeeAccountId = receipt.EmployeeAccountId
            }).ToList();

            return Ok(receiptDtos);
        }

        // GET: api/ReceiptsToEmployee/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ReceiptToEmployeeDTO>> GetReceiptToEmployee(int id)
        {
            var receipt = await _context.receiptsToEmployee.FindAsync(id);

            if (receipt == null)
            {
                return NotFound();
            }

            var receiptDto = new ReceiptToEmployeeDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                EmployeeAccountId = receipt.EmployeeAccountId
            };

            return Ok(receiptDto);
        }

        // GET: api/ReceiptsToEmployee/ByEmployeeAccount/5
        [HttpGet("ByEmployeeAccount/{employeeAccountId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReceiptToEmployeeDTO>>> GetReceiptsByEmployeeAccount(int employeeAccountId)
        {
            var receipts = await _context.receiptsToEmployee
                                         .Where(r => r.EmployeeAccountId == employeeAccountId)
                                         .ToListAsync();

            if (!receipts.Any())
            {
                return NotFound();
            }

            var receiptDtos = receipts.Select(receipt => new ReceiptToEmployeeDTO
            {
                Id = receipt.Id,
                Date = receipt.Date,
                Receiver = receipt.Receiver,
                Payer = receipt.Payer,
                Amount = receipt.Amount,
                EmployeeAccountId = receipt.EmployeeAccountId
            }).ToList();

            return Ok(receiptDtos);
        }

        // PUT: api/ReceiptsToEmployee/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReceiptToEmployee(int id, ReceiptToEmployeeDTO receiptDto)
        {
            if (id != receiptDto.Id)
            {
                return BadRequest();
            }

            var receipt = await _context.receiptsToEmployee.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            var previousAmount = receipt.Amount;

            receipt.Date = receiptDto.Date;
            receipt.Receiver = receiptDto.Receiver;
            receipt.Payer = receiptDto.Payer;
            receipt.Amount = receiptDto.Amount;
            receipt.EmployeeAccountId = receiptDto.EmployeeAccountId;

            _context.Entry(receipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var employeeAccount = await _context.employeeAccounts.FindAsync(receipt.EmployeeAccountId);
                if (employeeAccount != null)
                {
                    employeeAccount.Debt += previousAmount; // Revert previous amount
                    employeeAccount.Debt -= receipt.Amount; // Apply new amount
                    employeeAccount.TotalBalance -= previousAmount; // Revert previous amount
                    employeeAccount.Debt += receipt.Amount; // Apply new amount

                    _context.Entry(employeeAccount).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptToEmployeeExists(id))
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

        // POST: api/ReceiptsToEmployee
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReceiptToEmployeeDTO>> PostReceiptToEmployee(ReceiptToEmployeeDTO receiptDto)
        {
            var receipt = new ReceiptToEmployee
            {
                Date = receiptDto.Date,
                Receiver = receiptDto.Receiver,
                Payer = receiptDto.Payer,
                Amount = receiptDto.Amount,
                EmployeeAccountId = receiptDto.EmployeeAccountId
            };

            _context.receiptsToEmployee.Add(receipt);
            await _context.SaveChangesAsync();

            var employeeAccount = await _context.employeeAccounts.FindAsync(receipt.EmployeeAccountId);
            if (employeeAccount != null)
            {
                employeeAccount.Debt -= receipt.Amount;
                employeeAccount.TotalBalance += receipt.Amount;
                _context.Entry(employeeAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            receiptDto.Id = receipt.Id;

            return CreatedAtAction("GetReceiptToEmployee", new { id = receipt.Id }, receiptDto);
        }

        // DELETE: api/ReceiptsToEmployee/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReceiptToEmployee(int id)
        {
            var receipt = await _context.receiptsToEmployee.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            var amount = receipt.Amount;

            _context.receiptsToEmployee.Remove(receipt);
            await _context.SaveChangesAsync();

            var employeeAccount = await _context.employeeAccounts.FindAsync(receipt.EmployeeAccountId);
            if (employeeAccount != null)
            {
                employeeAccount.Debt += amount;
                employeeAccount.TotalBalance -= amount;
                _context.Entry(employeeAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool ReceiptToEmployeeExists(int id)
        {
            return _context.receiptsToEmployee.Any(e => e.Id == id);
        }
    }
}
