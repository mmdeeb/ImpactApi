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
    public class ClientAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClientAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientAccount>>> GetclientAccounts()
        {
            return await _context.clientAccounts.ToListAsync();
        }

        // GET: api/ClientAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientAccount>> GetClientAccount(int id)
        {
            var clientAccount = await _context.clientAccounts.FindAsync(id);

            if (clientAccount == null)
            {
                return NotFound();
            }

            return clientAccount;
        }

        // PUT: api/ClientAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientAccount(int id, ClientAccount clientAccount)
        {
            if (id != clientAccount.Id)
            {
                return BadRequest();
            }

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientAccount>> PostClientAccount(ClientAccount clientAccount)
        {
            _context.clientAccounts.Add(clientAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientAccount", new { id = clientAccount.Id }, clientAccount);
        }

        // DELETE: api/ClientAccounts/5
        [HttpDelete("{id}")]
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
