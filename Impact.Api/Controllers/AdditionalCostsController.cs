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
    public class AdditionalCostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdditionalCostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdditionalCosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalCostDTO>>> GetAdditionalCosts()
        {
            var additionalCosts = await _context.additionalCosts.ToListAsync();

            var additionalCostDtos = additionalCosts.Select(additionalCost => new AdditionalCostDTO
            {
                Id = additionalCost.Id,
                Cost = additionalCost.Cost,
                Detailes = additionalCost.Detailes,
                Date = additionalCost.Date,
                PhotoInvoiceURL = additionalCost.PhotoInvoiceURL,
                TrainingInvoiceId = additionalCost.TrainingInvoiceId
            }).ToList();

            return Ok(additionalCostDtos);
        }

        // GET: api/AdditionalCosts/ByInvoice/5
        [HttpGet("ByInvoice/{trainingInvoiceId}")]
        public async Task<ActionResult<IEnumerable<AdditionalCostDTO>>> GetAdditionalCostsByInvoice(int trainingInvoiceId)
        {
            var additionalCosts = await _context.additionalCosts
                                                .Where(ac => ac.TrainingInvoiceId == trainingInvoiceId)
                                                .ToListAsync();

            if (!additionalCosts.Any())
            {
                return NotFound();
            }

            var additionalCostDtos = additionalCosts.Select(additionalCost => new AdditionalCostDTO
            {
                Id = additionalCost.Id,
                Cost = additionalCost.Cost,
                Detailes = additionalCost.Detailes,
                Date = additionalCost.Date,
                PhotoInvoiceURL = additionalCost.PhotoInvoiceURL,
                TrainingInvoiceId = additionalCost.TrainingInvoiceId
            }).ToList();

            return Ok(additionalCostDtos);
        }

        // GET: api/AdditionalCosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalCostDTO>> GetAdditionalCost(int id)
        {
            var additionalCost = await _context.additionalCosts.FindAsync(id);

            if (additionalCost == null)
            {
                return NotFound();
            }

            var additionalCostDto = new AdditionalCostDTO
            {
                Id = additionalCost.Id,
                Cost = additionalCost.Cost,
                Detailes = additionalCost.Detailes,
                Date = additionalCost.Date,
                PhotoInvoiceURL = additionalCost.PhotoInvoiceURL,
                TrainingInvoiceId = additionalCost.TrainingInvoiceId
            };

            return Ok(additionalCostDto);
        }

        // PUT: api/AdditionalCosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalCost(int id, AdditionalCostDTO additionalCostDto)
        {
            if (id != additionalCostDto.Id)
            {
                return BadRequest();
            }

            var additionalCost = await _context.additionalCosts.FindAsync(id);
            if (additionalCost == null)
            {
                return NotFound();
            }

            var previousCost = additionalCost.Cost;

            additionalCost.Cost = additionalCostDto.Cost;
            additionalCost.Detailes = additionalCostDto.Detailes;
            additionalCost.Date = additionalCostDto.Date;
            additionalCost.PhotoInvoiceURL = additionalCostDto.PhotoInvoiceURL;
            additionalCost.TrainingInvoiceId = additionalCostDto.TrainingInvoiceId;

            _context.Entry(additionalCost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var trainingInvoice = await _context.trainingInvoices.FirstOrDefaultAsync(ti => ti.Id == additionalCost.TrainingInvoiceId);
                if (trainingInvoice != null)
                {
                    trainingInvoice.AllAdditionalCosts -= previousCost;
                    trainingInvoice.AllAdditionalCosts += additionalCost.Cost;
                    trainingInvoice.TotalCost -= previousCost;
                    trainingInvoice.TotalCost += additionalCost.Cost;

                    var clientAccount = await _context.clientAccounts.FirstOrDefaultAsync(ca => ca.Id == trainingInvoice.ClientAccountId);
                    if (clientAccount != null)
                    {
                        clientAccount.Debt -= previousCost;
                        clientAccount.Debt += additionalCost.Cost;
                        _context.Entry(clientAccount).State = EntityState.Modified;
                    }

                    _context.Entry(trainingInvoice).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalCostExists(id))
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

        // POST: api/AdditionalCosts
        [HttpPost]
        public async Task<ActionResult<AdditionalCostDTO>> PostAdditionalCost(AdditionalCostDTO additionalCostDto)
        {
            var additionalCost = new AdditionalCost
            {
                Cost = additionalCostDto.Cost,
                Detailes = additionalCostDto.Detailes,
                Date = additionalCostDto.Date,
                PhotoInvoiceURL = additionalCostDto.PhotoInvoiceURL,
                TrainingInvoiceId = additionalCostDto.TrainingInvoiceId
            };

            _context.additionalCosts.Add(additionalCost);
            await _context.SaveChangesAsync();

            var trainingInvoice = await _context.trainingInvoices.FirstOrDefaultAsync(ti => ti.Id == additionalCost.TrainingInvoiceId);
            if (trainingInvoice != null)
            {
                trainingInvoice.AllAdditionalCosts += additionalCost.Cost;
                trainingInvoice.TotalCost += additionalCost.Cost;

                var clientAccount = await _context.clientAccounts.FirstOrDefaultAsync(ca => ca.Id == trainingInvoice.ClientAccountId);
                if (clientAccount != null)
                {
                    clientAccount.Debt += additionalCost.Cost;
                    _context.Entry(clientAccount).State = EntityState.Modified;
                }

                _context.Entry(trainingInvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            additionalCostDto.Id = additionalCost.Id;

            return CreatedAtAction("GetAdditionalCost", new { id = additionalCost.Id }, additionalCostDto);
        }

        // DELETE: api/AdditionalCosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalCost(int id)
        {
            var additionalCost = await _context.additionalCosts.FindAsync(id);
            if (additionalCost == null)
            {
                return NotFound();
            }

            var cost = additionalCost.Cost;
            var trainingInvoice = await _context.trainingInvoices.FirstOrDefaultAsync(ti => ti.Id == additionalCost.TrainingInvoiceId);

            _context.additionalCosts.Remove(additionalCost);
            await _context.SaveChangesAsync();

            if (trainingInvoice != null)
            {
                trainingInvoice.AllAdditionalCosts -= cost;
                trainingInvoice.TotalCost -= cost;

                var clientAccount = await _context.clientAccounts.FirstOrDefaultAsync(ca => ca.Id == trainingInvoice.ClientAccountId);
                if (clientAccount != null)
                {
                    clientAccount.Debt -= cost;
                    _context.Entry(clientAccount).State = EntityState.Modified;
                }

                _context.Entry(trainingInvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool AdditionalCostExists(int id)
        {
            return _context.additionalCosts.Any(e => e.Id == id);
        }
    }
}
