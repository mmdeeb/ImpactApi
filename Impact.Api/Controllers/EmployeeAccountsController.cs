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
    public class EmployeeAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeAccount>>> GetemployeeAccounts()
        {
            return await _context.employeeAccounts.ToListAsync();
        }

        // GET: api/EmployeeAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAccount>> GetEmployeeAccount(int id)
        {
            var employeeAccount = await _context.employeeAccounts.FindAsync(id);

            if (employeeAccount == null)
            {
                return NotFound();
            }

            return employeeAccount;
        }

        // PUT: api/EmployeeAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAccount(int id, EmployeeAccount employeeAccount)
        {
            if (id != employeeAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAccountExists(id))
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

        // POST: api/EmployeeAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeAccount>> PostEmployeeAccount(EmployeeAccount employeeAccount)
        {
            _context.employeeAccounts.Add(employeeAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeAccount", new { id = employeeAccount.Id }, employeeAccount);
        }

        // DELETE: api/EmployeeAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAccount(int id)
        {
            var employeeAccount = await _context.employeeAccounts.FindAsync(id);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            _context.employeeAccounts.Remove(employeeAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeAccountExists(int id)
        {
            return _context.employeeAccounts.Any(e => e.Id == id);
        }
    }
}
