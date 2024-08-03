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
    public class OtherExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OtherExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OtherExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtherExpensesDTO>>> GetOtherExpenses()
        {
            var expenses = await _context.otherExpenses.ToListAsync();

            var expensesDtos = expenses.Select(expense => new OtherExpensesDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                PhotoInvoiceURL = expense.PhotoInvoiceURL,
                Date = expense.Date,
                Amount = expense.Amount,
                EmployeeName = expense.EmployeeName,
                CenterId = expense.CenterId
            }).ToList();

            return Ok(expensesDtos);
        }

        // GET: api/OtherExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OtherExpensesDTO>> GetOtherExpense(int id)
        {
            var expense = await _context.otherExpenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            var expenseDto = new OtherExpensesDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                PhotoInvoiceURL = expense.PhotoInvoiceURL,
                Date = expense.Date,
                Amount = expense.Amount,
                EmployeeName = expense.EmployeeName,
                CenterId = expense.CenterId
            };

            return Ok(expenseDto);
        }

        // GET: api/OtherExpenses/ByCenter/5
        [HttpGet("ByCenter/{centerId}")]
        public async Task<ActionResult<IEnumerable<OtherExpensesDTO>>> GetOtherExpensesByCenter(int centerId)
        {
            var expenses = await _context.otherExpenses
                                         .Where(e => e.CenterId == centerId)
                                         .ToListAsync();

            if (!expenses.Any())
            {
                return NotFound();
            }

            var expensesDtos = expenses.Select(expense => new OtherExpensesDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                PhotoInvoiceURL = expense.PhotoInvoiceURL,
                Date = expense.Date,
                Amount = expense.Amount,
                EmployeeName = expense.EmployeeName,
                CenterId = expense.CenterId
            }).ToList();

            return Ok(expensesDtos);
        }

        // PUT: api/OtherExpenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtherExpense(int id, OtherExpensesDTO expenseDto)
        {
            if (id != expenseDto.Id)
            {
                return BadRequest();
            }

            var expense = await _context.otherExpenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            expense.Description = expenseDto.Description;
            expense.PhotoInvoiceURL = expenseDto.PhotoInvoiceURL;
            expense.Date = expenseDto.Date;
            expense.Amount = expenseDto.Amount;
            expense.EmployeeName = expenseDto.EmployeeName;
            expense.CenterId = expenseDto.CenterId;

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtherExpenseExists(id))
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

        // POST: api/OtherExpenses
        [HttpPost]
        public async Task<ActionResult<OtherExpensesDTO>> PostOtherExpense(OtherExpensesDTO expenseDto)
        {
            var expense = new OtherExpenses
            {
                Description = expenseDto.Description,
                PhotoInvoiceURL = expenseDto.PhotoInvoiceURL,
                Date = expenseDto.Date,
                Amount = expenseDto.Amount,
                EmployeeName = expenseDto.EmployeeName,
                CenterId = expenseDto.CenterId
            };

            _context.otherExpenses.Add(expense);
            await _context.SaveChangesAsync();

            expenseDto.Id = expense.Id;

            return CreatedAtAction("GetOtherExpense", new { id = expense.Id }, expenseDto);
        }

        // DELETE: api/OtherExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtherExpense(int id)
        {
            var expense = await _context.otherExpenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.otherExpenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OtherExpenseExists(int id)
        {
            return _context.otherExpenses.Any(e => e.Id == id);
        }
    }
}
