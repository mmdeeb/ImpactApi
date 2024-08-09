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
    public class TrainingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Trainings
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainings()
        {
            var trainings = await _context.trainings.ToListAsync();

            var trainingDtos = trainings.Select(training => new TrainingDTO
            {
                Id = training.Id,
                TrainingName = training.TrainingName,
                NumberOfStudents = training.NumberOfStudents,
                TrainingDetails = training.TrainingDetails,
                TrainingInvoiceId = training.TrainingInvoiceId,
                ClientId = training.ClientId
            }).ToList();

            return Ok(trainingDtos);
        }

        // GET: api/Trainings/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TrainingDTO>> GetTraining(int id)
        {
            var training = await _context.trainings.FindAsync(id);

            if (training == null)
            {
                return NotFound();
            }

            var trainingDto = new TrainingDTO
            {
                Id = training.Id,
                TrainingName = training.TrainingName,
                NumberOfStudents = training.NumberOfStudents,
                TrainingDetails = training.TrainingDetails,
                TrainingInvoiceId = training.TrainingInvoiceId,
                ClientId = training.ClientId
            };

            return Ok(trainingDto);
        }

        // GET: api/Trainings/ByClient/5
        [HttpGet("ByClient/{clientId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainingsByClient(int clientId)
        {
            var trainings = await _context.trainings
                                          .Where(t => t.ClientId == clientId)
                                          .ToListAsync();

            if (trainings == null || !trainings.Any())
            {
                return NotFound();
            }

            var trainingDtos = trainings.Select(training => new TrainingDTO
            {
                Id = training.Id,
                TrainingName = training.TrainingName,
                NumberOfStudents = training.NumberOfStudents,
                TrainingDetails = training.TrainingDetails,
                TrainingInvoiceId = training.TrainingInvoiceId,
                ClientId = training.ClientId
            }).ToList();

            return Ok(trainingDtos);
        }

        // GET: api/Trainings/ByInvoice/5
        [HttpGet("ByInvoice/{trainingInvoiceId}")]
        [Authorize]
        public async Task<ActionResult<TrainingDTO>> GetTrainingByInvoice(int trainingInvoiceId)
        {
            var training = await _context.trainings
                                         .FirstOrDefaultAsync(t => t.TrainingInvoiceId == trainingInvoiceId);

            if (training == null)
            {
                return NotFound();
            }

            var trainingDto = new TrainingDTO
            {
                Id = training.Id,
                TrainingName = training.TrainingName,
                NumberOfStudents = training.NumberOfStudents,
                TrainingDetails = training.TrainingDetails,
                TrainingInvoiceId = training.TrainingInvoiceId,
                ClientId = training.ClientId
            };

            return Ok(trainingDto);
        }
 
        // PUT: api/Trainings/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTraining(int id, TrainingDTO trainingDto)
        {
            if (id != trainingDto.Id)
            {
                return BadRequest();
            }

            var training = await _context.trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            training.TrainingName = trainingDto.TrainingName;
            training.NumberOfStudents = trainingDto.NumberOfStudents;
            training.TrainingDetails = trainingDto.TrainingDetails;

            _context.Entry(training).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingExists(id))
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

        // POST: api/Trainings
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TrainingDTO>> PostTraining(TrainingDTO trainingDto)
        {
            var client = await _context.clients.FindAsync(trainingDto.ClientId);
            if (client == null)
            {
                return BadRequest(new { Message = "Client not found" });
            }

            var trainingInvoice = new TrainingInvoice
            {
                MealsCost = 0,
                TrainerCost = 0,
                PhotoInvoiceURL = null,
                ReservationsCost = 0,
                TotalCost = 0,
                Discount = 0,
                FinalCost = 0,
                ClientAccountId = client.ClientAccountId
            };

            _context.trainingInvoices.Add(trainingInvoice);
            await _context.SaveChangesAsync();

            var training = new Training
            {
                TrainingName = trainingDto.TrainingName,
                NumberOfStudents = trainingDto.NumberOfStudents,
                TrainingDetails = trainingDto.TrainingDetails,
                TrainingInvoiceId = trainingInvoice.Id,
                ClientId = trainingDto.ClientId
            };

            _context.trainings.Add(training);
            await _context.SaveChangesAsync();

            trainingDto.Id = training.Id;
            trainingDto.TrainingInvoiceId = trainingInvoice.Id;

            return CreatedAtAction("GetTraining", new { id = training.Id }, trainingDto);
        }

        // DELETE: api/Trainings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            var training = await _context.trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            _context.trainings.Remove(training);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingExists(int id)
        {
            return _context.trainings.Any(e => e.Id == id);
        }
    }
}
