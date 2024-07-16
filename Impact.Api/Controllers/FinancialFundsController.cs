using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ImpactBackend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialFundsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinancialFundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FinancialFunds
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FinancialFund>>> GetfinancialFunds()
        {
            return await _context.financialFunds.ToListAsync();
        }

        // GET: api/FinancialFunds/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<FinancialFund>> GetFinancialFund(int id)
        {
            var financialFund = await _context.financialFunds.FindAsync(id);

            if (financialFund == null)
            {
                return NotFound();
            }

            return financialFund;
        }

        // PUT: api/FinancialFunds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutFinancialFund(int id, FinancialFund financialFund)
        {
            if (id != financialFund.Id)
            {
                return BadRequest();
            }

            _context.Entry(financialFund).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialFundExists(id))
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

        // POST: api/FinancialFunds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<FinancialFund>> PostFinancialFund(FinancialFund financialFund)
        {
            _context.financialFunds.Add(financialFund);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancialFund", new { id = financialFund.Id }, financialFund);
        }

        // DELETE: api/FinancialFunds/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFinancialFund(int id)
        {
            var financialFund = await _context.financialFunds.FindAsync(id);
            if (financialFund == null)
            {
                return NotFound();
            }

            _context.financialFunds.Remove(financialFund);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancialFundExists(int id)
        {
            return _context.financialFunds.Any(e => e.Id == id);
        }
    }
}
