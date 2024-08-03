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

            var trainingInvoiceDtos = trainingInvoices.Select(invoice =>
            {
                double calculatedFinalCost = invoice.TotalCost - invoice.Discount;
                if (invoice.FinalCost != calculatedFinalCost)
                {
                    invoice.FinalCost = calculatedFinalCost;
                    _context.Entry(invoice).State = EntityState.Modified;
                }

                return new TrainingInvoiceDTO
                {
                    Id = invoice.Id,
                    MealsCost = invoice.MealsCost,
                    TrainerCost = invoice.TrainerCost,
                    PhotoInvoiceURL = invoice.PhotoInvoiceURL,
                    ReservationsCost = invoice.ReservationsCost,
                    AllAdditionalCosts = invoice.AllAdditionalCosts,
                    TotalCost = invoice.TotalCost,
                    Discount = invoice.Discount,
                    FinalCost = invoice.FinalCost,
                    ClientAccountId = invoice.ClientAccountId
                };
            }).ToList();

            await _context.SaveChangesAsync(); // Save changes to FinalCost if updated

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

            double calculatedFinalCost = trainingInvoice.TotalCost - trainingInvoice.Discount;
            if (trainingInvoice.FinalCost != calculatedFinalCost)
            {
                trainingInvoice.FinalCost = calculatedFinalCost;
                _context.Entry(trainingInvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync(); // Save changes to FinalCost if updated
            }

            var trainingInvoiceDto = new TrainingInvoiceDTO
            {
                Id = trainingInvoice.Id,
                MealsCost = trainingInvoice.MealsCost,
                TrainerCost = trainingInvoice.TrainerCost,
                PhotoInvoiceURL = trainingInvoice.PhotoInvoiceURL,
                ReservationsCost = trainingInvoice.ReservationsCost,
                AllAdditionalCosts = trainingInvoice.AllAdditionalCosts,
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
            trainingInvoice.AllAdditionalCosts = trainingInvoiceDto.AllAdditionalCosts;
            trainingInvoice.TotalCost = trainingInvoiceDto.TotalCost;
            trainingInvoice.Discount = trainingInvoiceDto.Discount;
            trainingInvoice.FinalCost = trainingInvoice.TotalCost - trainingInvoiceDto.Discount;  
            
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

        // PATCH: api/TrainingInvoices/UpdateDiscount/5
        [HttpPatch("UpdateDiscount/{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, [FromBody] double discount)
        {
            var trainingInvoice = await _context.trainingInvoices.FindAsync(id);
            if (trainingInvoice == null)
            {
                return NotFound();
            }

            trainingInvoice.Discount = discount;
            trainingInvoice.FinalCost = trainingInvoice.TotalCost - discount;
            
            _context.Entry(trainingInvoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return NoContent();
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
