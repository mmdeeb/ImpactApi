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
    public class ReceiptFromClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceiptFromClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptFromClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptFromClient>>> GetreceiptFromClients()
        {
            return await _context.receiptFromClients.ToListAsync();
        }

        // GET: api/ReceiptFromClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptFromClient>> GetReceiptFromClient(int id)
        {
            var receiptFromClient = await _context.receiptFromClients.FindAsync(id);

            if (receiptFromClient == null)
            {
                return NotFound();
            }

            return receiptFromClient;
        }

        // PUT: api/ReceiptFromClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptFromClient(int id, ReceiptFromClient receiptFromClient)
        {
            if (id != receiptFromClient.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptFromClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/ReceiptFromClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceiptFromClient>> PostReceiptFromClient(ReceiptFromClient receiptFromClient)
        {
            _context.receiptFromClients.Add(receiptFromClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiptFromClient", new { id = receiptFromClient.Id }, receiptFromClient);
        }

        // DELETE: api/ReceiptFromClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiptFromClient(int id)
        {
            var receiptFromClient = await _context.receiptFromClients.FindAsync(id);
            if (receiptFromClient == null)
            {
                return NotFound();
            }

            _context.receiptFromClients.Remove(receiptFromClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptFromClientExists(int id)
        {
            return _context.receiptFromClients.Any(e => e.Id == id);
        }
    }
}
