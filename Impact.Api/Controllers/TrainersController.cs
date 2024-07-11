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
    public class TrainersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
        {
            return await _context.trainers.ToListAsync();
        }

        // GET: api/Trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> GetTrainer(int id)
        {
            var trainer = await _context.trainers.FindAsync(id);

            if (trainer == null)
            {
                return NotFound();
            }

            return trainer;
        }

        // GET: api/Trainers/GetTrainersBySubTraining/5
        [HttpGet("GetTrainersBySubTraining/{subTrainingId}")]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainersBySubTraining(int subTrainingId)
        {
            var trainers = await _context.subTrainings
                                         .Where(st => st.Id == subTrainingId)
                                         .SelectMany(st => st.Trainers)
                                         .ToListAsync();

            if (trainers == null || !trainers.Any())
            {
                return NotFound();
            }

            return Ok(trainers);
        }

        // PUT: api/Trainers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, Trainer trainer)
        {
            if (id != trainer.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(id))
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

        // POST: api/Trainers
        [HttpPost]
        public async Task<ActionResult<Trainer>> PostTrainer(Trainer trainer)
        {
            _context.trainers.Add(trainer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainer", new { id = trainer.Id }, trainer);
        }


        // POST: api/Trainers/5/AddSubTrainings
        [HttpPost("{trainerId}/AddSubTrainings")]
        public async Task<IActionResult> AddSubTrainingsToTrainer(int trainerId, [FromBody] List<int> subTrainingIds)
        {
            var trainer = await _context.trainers
                                        .Include(t => t.SubTraining)
                                        .FirstOrDefaultAsync(t => t.Id == trainerId);

            if (trainer == null)
            {
                return NotFound();
            }

            if (trainer.SubTraining == null)
            {
                trainer.SubTraining = new List<SubTraining>();
            }

            var subTrainings = await _context.subTrainings.Where(st => subTrainingIds.Contains(st.Id)).ToListAsync();

            foreach (var subTraining in subTrainings)
            {
                if (!trainer.SubTraining.Contains(subTraining))
                {
                    trainer.SubTraining.Add(subTraining);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Trainers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainer = await _context.trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _context.trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerExists(int id)
        {
            return _context.trainers.Any(e => e.Id == id);
        }
    }
}
