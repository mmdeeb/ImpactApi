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
        public async Task<ActionResult<IEnumerable<EmployeeAccountDTO>>> GetEmployeeAccounts()
        {
            var employeeAccounts = await _context.employeeAccounts.ToListAsync();

            var employeeAccountDtos = employeeAccounts.Select(account => new EmployeeAccountDTO
            {
                Id = account.Id,
                Deduct = account.Deduct,
                AdvancePayment = account.AdvancePayment,
                Reward = account.Reward,
                TotalBalance = account.TotalBalance,
                Debt = account.Debt
            }).ToList();

            return Ok(employeeAccountDtos);
        }

        // GET: api/EmployeeAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAccountDTO>> GetEmployeeAccount(int id)
        {
            var employeeAccount = await _context.employeeAccounts.FindAsync(id);

            if (employeeAccount == null)
            {
                return NotFound();
            }

            var employeeAccountDto = new EmployeeAccountDTO
            {
                Id = employeeAccount.Id,
                Deduct = employeeAccount.Deduct,
                AdvancePayment = employeeAccount.AdvancePayment,
                Reward = employeeAccount.Reward,
                TotalBalance = employeeAccount.TotalBalance,
                Debt = employeeAccount.Debt
            };

            return Ok(employeeAccountDto);
        }

        // PUT: api/EmployeeAccounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAccount(int id, EmployeeAccountDTO employeeAccountDto)
        {
            if (id != employeeAccountDto.Id)
            {
                return BadRequest();
            }

            var employeeAccount = await _context.employeeAccounts.FindAsync(id);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            employeeAccount.Deduct = employeeAccountDto.Deduct;
            employeeAccount.AdvancePayment = employeeAccountDto.AdvancePayment;
            employeeAccount.Reward = employeeAccountDto.Reward;
            employeeAccount.TotalBalance = employeeAccountDto.TotalBalance;
            employeeAccount.Debt = employeeAccountDto.Debt;

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
        [HttpPost]
        public async Task<ActionResult<EmployeeAccountDTO>> PostEmployeeAccount(EmployeeAccountDTO employeeAccountDto)
        {
            var employeeAccount = new EmployeeAccount
            {
                Deduct = employeeAccountDto.Deduct,
                AdvancePayment = employeeAccountDto.AdvancePayment,
                Reward = employeeAccountDto.Reward,
                TotalBalance = employeeAccountDto.TotalBalance,
                Debt = employeeAccountDto.Debt
            };

            _context.employeeAccounts.Add(employeeAccount);
            await _context.SaveChangesAsync();

            employeeAccountDto.Id = employeeAccount.Id;

            return CreatedAtAction("GetEmployeeAccount", new { id = employeeAccount.Id }, employeeAccountDto);
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

        // PATCH: api/EmployeeAccounts/AddDeduct/5
        [HttpPatch("AddDeduct/{id}")]
        public async Task<IActionResult> AddDeduct(int id, [FromBody] double deduct)
        {
            var employeeAccount = await _context.employeeAccounts.FindAsync(id);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            employeeAccount.Debt -= deduct;
            employeeAccount.Deduct += deduct;

            _context.Entry(employeeAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/EmployeeAccounts/AddAdvancePayment/5
        [HttpPatch("AddAdvancePayment/{id}")]
        public async Task<IActionResult> AddAdvancePayment(int id, [FromBody] double advancePayment)
        {
            var employeeAccount = await _context.employeeAccounts.FindAsync(id);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            employeeAccount.Debt -= advancePayment;
            employeeAccount.AdvancePayment += advancePayment;

            _context.Entry(employeeAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/EmployeeAccounts/AddReward/5
        [HttpPatch("AddReward/{id}")]
        public async Task<IActionResult> AddReward(int id, [FromBody] double reward)
        {
            var employeeAccount = await _context.employeeAccounts.FindAsync(id);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            employeeAccount.Debt += reward;
            employeeAccount.Reward += reward;

            _context.Entry(employeeAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeAccountExists(int id)
        {
            return _context.employeeAccounts.Any(e => e.Id == id);
        }
    }
}
