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
    public class ReceiptToEmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceiptToEmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptToEmployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptToEmployee>>> GetreceiptToEmployees()
        {
            return await _context.receiptToEmployees.ToListAsync();
        }

        // GET: api/ReceiptToEmployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptToEmployee>> GetReceiptToEmployee(int id)
        {
            var receiptToEmployee = await _context.receiptToEmployees.FindAsync(id);

            if (receiptToEmployee == null)
            {
                return NotFound();
            }

            return receiptToEmployee;
        }

        // PUT: api/ReceiptToEmployees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptToEmployee(int id, ReceiptToEmployee receiptToEmployee)
        {
            if (id != receiptToEmployee.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptToEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/ReceiptToEmployees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceiptToEmployee>> PostReceiptToEmployee(ReceiptToEmployee receiptToEmployee)
        {
            _context.receiptToEmployees.Add(receiptToEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiptToEmployee", new { id = receiptToEmployee.Id }, receiptToEmployee);
        }

        // DELETE: api/ReceiptToEmployees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiptToEmployee(int id)
        {
            var receiptToEmployee = await _context.receiptToEmployees.FindAsync(id);
            if (receiptToEmployee == null)
            {
                return NotFound();
            }

            _context.receiptToEmployees.Remove(receiptToEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptToEmployeeExists(int id)
        {
            return _context.receiptToEmployees.Any(e => e.Id == id);
        }
    }
}
