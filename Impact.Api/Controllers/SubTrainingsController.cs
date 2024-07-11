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
    public class SubTrainingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubTrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubTrainings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTraining>>> GetSubTrainings()
        {
            return await _context.subTrainings.ToListAsync();
        }

        // GET: api/SubTrainings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubTraining>> GetSubTraining(int id)
        {
            var subTraining = await _context.subTrainings.FindAsync(id);

            if (subTraining == null)
            {
                return NotFound();
            }

            return subTraining;
        }

        // GET: api/SubTrainings/GetSubTrainingsByTrainer/5
        [HttpGet("GetSubTrainingsByTrainer/{trainerId}")]
        public async Task<ActionResult<IEnumerable<SubTraining>>> GetSubTrainingsByTrainer(int trainerId)
        {
            var subTrainings = await _context.trainers
                                             .Where(t => t.Id == trainerId)
                                             .SelectMany(t => t.SubTraining)
                                             .ToListAsync();

            if (subTrainings == null || !subTrainings.Any())
            {
                return NotFound();
            }

            return Ok(subTrainings);
        }

        // PUT: api/SubTrainings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubTraining(int id, SubTraining subTraining)
        {
            if (id != subTraining.Id)
            {
                return BadRequest();
            }

            _context.Entry(subTraining).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubTrainingExists(id))
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

        // POST: api/SubTrainings
        [HttpPost]
        public async Task<ActionResult<SubTraining>> PostSubTraining(SubTraining subTraining)
        {
            _context.subTrainings.Add(subTraining);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubTraining", new { id = subTraining.Id }, subTraining);
        }


        // POST: api/SubTrainings/5/AddTrainers
        [HttpPost("{subTrainingId}/AddTrainers")]
        public async Task<IActionResult> AddTrainersToSubTraining(int subTrainingId, [FromBody] List<int> trainerIds)
        {
            var subTraining = await _context.subTrainings
                                             .Include(st => st.Trainers)
                                             .FirstOrDefaultAsync(st => st.Id == subTrainingId);

            if (subTraining == null)
            {
                return NotFound();
            }

            if (subTraining.Trainers == null)
            {
                subTraining.Trainers = new List<Trainer>();
            }

            var trainers = await _context.trainers.Where(t => trainerIds.Contains(t.Id)).ToListAsync();

            foreach (var trainer in trainers)
            {
                if (!subTraining.Trainers.Contains(trainer))
                {
                    subTraining.Trainers.Add(trainer);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/SubTrainings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTraining(int id)
        {
            var subTraining = await _context.subTrainings.FindAsync(id);
            if (subTraining == null)
            {
                return NotFound();
            }

            _context.subTrainings.Remove(subTraining);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubTrainingExists(int id)
        {
            return _context.subTrainings.Any(e => e.Id == id);
        }
    }
}
