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
    public class TrainingInvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingInvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingInvoiceDTO>>> GetTrainingInvoices()
        {
            var trainingInvoices = await _context.trainingInvoices.ToListAsync();

            var trainingInvoiceDtos = trainingInvoices.Select(invoice => new TrainingInvoiceDTO
            {
                Id = invoice.Id,
                MealsCost = invoice.MealsCost,
                TrainerCost = invoice.TrainerCost,
                PhotoInvoiceURL = invoice.PhotoInvoiceURL,
                ReservationsCost = invoice.ReservationsCost,
                TotalCost = invoice.TotalCost,
                Discount = invoice.Discount,
                FinalCost = invoice.FinalCost,
                ClientAccountId = invoice.ClientAccountId
            }).ToList();

            return Ok(trainingInvoiceDtos);
        }

        // GET: api/TrainingInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingInvoiceDTO>> GetTrainingInvoice(int id)
        {
            var trainingInvoice = await _context.trainingInvoices.FindAsync(id);

            if (trainingInvoice == null)
            {
                return NotFound();
            }

            var trainingInvoiceDto = new TrainingInvoiceDTO
            {
                Id = trainingInvoice.Id,
                MealsCost = trainingInvoice.MealsCost,
                TrainerCost = trainingInvoice.TrainerCost,
                PhotoInvoiceURL = trainingInvoice.PhotoInvoiceURL,
                ReservationsCost = trainingInvoice.ReservationsCost,
                TotalCost = trainingInvoice.TotalCost,
                Discount = trainingInvoice.Discount,
                FinalCost = trainingInvoice.FinalCost,
                ClientAccountId = trainingInvoice.ClientAccountId
            };

            return Ok(trainingInvoiceDto);
        }

        // PUT: api/TrainingInvoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingInvoice(int id, TrainingInvoiceDTO trainingInvoiceDto)
        {
            if (id != trainingInvoiceDto.Id)
            {
                return BadRequest();
            }

            var trainingInvoice = await _context.trainingInvoices.FindAsync(id);
            if (trainingInvoice == null)
            {
                return NotFound();
            }

            trainingInvoice.MealsCost = trainingInvoiceDto.MealsCost;
            trainingInvoice.TrainerCost = trainingInvoiceDto.TrainerCost;
            trainingInvoice.PhotoInvoiceURL = trainingInvoiceDto.PhotoInvoiceURL;
            trainingInvoice.ReservationsCost = trainingInvoiceDto.ReservationsCost;
            trainingInvoice.TotalCost = trainingInvoiceDto.TotalCost;
            trainingInvoice.Discount = trainingInvoiceDto.Discount;
            trainingInvoice.FinalCost = trainingInvoiceDto.FinalCost;
            trainingInvoice.ClientAccountId = trainingInvoiceDto.ClientAccountId;

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
//يجب حذفها
        // POST: api/TrainingInvoices
        [HttpPost]
        public async Task<ActionResult<TrainingInvoiceDTO>> PostTrainingInvoice(TrainingInvoiceDTO trainingInvoiceDto)
        {
            var trainingInvoice = new TrainingInvoice
            {
                MealsCost = trainingInvoiceDto.MealsCost,
                TrainerCost = trainingInvoiceDto.TrainerCost,
                PhotoInvoiceURL = trainingInvoiceDto.PhotoInvoiceURL,
                ReservationsCost = trainingInvoiceDto.ReservationsCost,
                TotalCost = trainingInvoiceDto.TotalCost,
                Discount = trainingInvoiceDto.Discount,
                FinalCost = trainingInvoiceDto.FinalCost,
                ClientAccountId = trainingInvoiceDto.ClientAccountId
            };

            _context.trainingInvoices.Add(trainingInvoice);
            await _context.SaveChangesAsync();

            trainingInvoiceDto.Id = trainingInvoice.Id;

            return CreatedAtAction("GetTrainingInvoice", new { id = trainingInvoice.Id }, trainingInvoiceDto);
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
