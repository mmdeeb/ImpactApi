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
    [Authorize]
    public class ClientAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClientAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientAccountDTO>>> GetClientAccounts()
        {
            var clientAccounts = await _context.clientAccounts.ToListAsync();

            var clientAccountDtos = clientAccounts.Select(clientAccount => new ClientAccountDTO
            {
                Id = clientAccount.Id,
                Discount = clientAccount.Discount,
                TotalBalance = clientAccount.TotalBalance,
                Debt = clientAccount.Debt
            }).ToList();

            return Ok(clientAccountDtos);
        }

        // GET: api/ClientAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientAccountDTO>> GetClientAccount(int id)
        {
            var clientAccount = await _context.clientAccounts.FindAsync(id);

            if (clientAccount == null)
            {
                return NotFound();
            }

            var clientAccountDto = new ClientAccountDTO
            {
                Id = clientAccount.Id,
                Discount = clientAccount.Discount,
                TotalBalance = clientAccount.TotalBalance,
                Debt = clientAccount.Debt
            };

            return Ok(clientAccountDto);
        }

        // PUT: api/ClientAccounts/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClientAccount(int id, ClientAccountDTO clientAccountDto)
        {
            if (id != clientAccountDto.Id)
            {
                return BadRequest();
            }

            var clientAccount = await _context.clientAccounts.FindAsync(id);
            if (clientAccount == null)
            {
                return NotFound();
            }

            clientAccount.Discount = clientAccountDto.Discount;
            clientAccount.TotalBalance = clientAccountDto.TotalBalance;
            clientAccount.Debt = clientAccountDto.Debt;

            _context.Entry(clientAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientAccountExists(id))
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

        // POST: api/ClientAccounts
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClientAccountDTO>> PostClientAccount(ClientAccountDTO clientAccountDto)
        {
            var clientAccount = new ClientAccount
            {
                Discount = clientAccountDto.Discount,
                TotalBalance = clientAccountDto.TotalBalance,
                Debt = clientAccountDto.Debt
            };

            _context.clientAccounts.Add(clientAccount);
            await _context.SaveChangesAsync();

            clientAccountDto.Id = clientAccount.Id;

            return CreatedAtAction("GetClientAccount", new { id = clientAccount.Id }, clientAccountDto);
        }

        // DELETE: api/ClientAccounts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClientAccount(int id)
        {
            var clientAccount = await _context.clientAccounts.FindAsync(id);
            if (clientAccount == null)
            {
                return NotFound();
            }

            _context.clientAccounts.Remove(clientAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientAccountExists(int id)
        {
            return _context.clientAccounts.Any(e => e.Id == id);
        }
    }
}
