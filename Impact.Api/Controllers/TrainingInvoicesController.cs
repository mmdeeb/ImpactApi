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
    public class TrainingInvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingInvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingInvoice>>> GettrainingInvoices()
        {
            return await _context.trainingInvoices.ToListAsync();
        }

        // GET: api/TrainingInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingInvoice>> GetTrainingInvoice(int id)
        {
            var trainingInvoice = await _context.trainingInvoices.FindAsync(id);

            if (trainingInvoice == null)
            {
                return NotFound();
            }

            return trainingInvoice;
        }

        // PUT: api/TrainingInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingInvoice(int id, TrainingInvoice trainingInvoice)
        {
            if (id != trainingInvoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingInvoiceExists(id))
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

        // POST: api/TrainingInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainingInvoice>> PostTrainingInvoice(TrainingInvoice trainingInvoice)
        {
            _context.trainingInvoices.Add(trainingInvoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingInvoice", new { id = trainingInvoice.Id }, trainingInvoice);
        }

        // DELETE: api/TrainingInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingInvoice(int id)
        {
            var trainingInvoice = await _context.trainingInvoices.FindAsync(id);
            if (trainingInvoice == null)
            {
                return NotFound();
            }

            _context.trainingInvoices.Remove(trainingInvoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingInvoiceExists(int id)
        {
            return _context.trainingInvoices.Any(e => e.Id == id);
        }
    }
}
