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
    public class TrainersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDTO>>> GetTrainers()
        {
            var trainers = await _context.trainers.ToListAsync();

            var trainerDtos = trainers.Select(t => new TrainerDTO
            {
                Id = t.Id,
                TrainerName = t.TrainerName,
                ImgLink = t.ImgLink,
                ListSkills = t.ListSkills,
                TrainerSpecialization = t.TrainerSpecialization,
                Summary = t.Summary,
                CV = t.CV
            }).ToList();

            return Ok(trainerDtos);
        }

        // GET: api/Trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDTO>> GetTrainer(int id)
        {
            var trainer = await _context.trainers.FirstOrDefaultAsync(t => t.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }

            var trainerDto = new TrainerDTO
            {
                Id = trainer.Id,
                TrainerName = trainer.TrainerName,
                ImgLink = trainer.ImgLink,
                ListSkills = trainer.ListSkills,
                TrainerSpecialization = trainer.TrainerSpecialization,
                Summary = trainer.Summary,
                CV = trainer.CV
            };

            return Ok(trainerDto);
        }

        // GET: api/Trainers/GetTrainersBySubTraining/5
        [HttpGet("GetTrainersBySubTraining/{subTrainingId}")]
        public async Task<ActionResult<IEnumerable<TrainerDTO>>> GetTrainersBySubTraining(int subTrainingId)
        {
            var trainers = await _context.subTrainings
                                         .Where(st => st.Id == subTrainingId)
                                         .SelectMany(st => st.Trainers)
                                         .Include(t => t.SubTraining)
                                         .ToListAsync();

            if (trainers == null || !trainers.Any())
            {
                return NotFound();
            }

            var trainerDtos = trainers.Select(t => new TrainerDTO
            {
                Id = t.Id,
                TrainerName = t.TrainerName,
                ImgLink = t.ImgLink,
                ListSkills = t.ListSkills,
                TrainerSpecialization = t.TrainerSpecialization,
                Summary = t.Summary,
                CV = t.CV
            }).ToList();

            return Ok(trainerDtos);
        }

        // PUT: api/Trainers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, TrainerDTO trainerDto)
        {
            if (id != trainerDto.Id)
            {
                return BadRequest();
            }

            var trainer = await _context.trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            trainer.TrainerName = trainerDto.TrainerName;
            trainer.ImgLink = trainerDto.ImgLink;
            trainer.ListSkills = trainerDto.ListSkills;
            trainer.TrainerSpecialization = trainerDto.TrainerSpecialization;
            trainer.Summary = trainerDto.Summary;
            trainer.CV = trainerDto.CV;

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
        public async Task<ActionResult<TrainerDTO>> PostTrainer(TrainerDTO trainerDto)
        {
            var trainer = new Trainer
            {
                TrainerName = trainerDto.TrainerName,
                ListSkills = trainerDto.ListSkills,
                ImgLink = trainerDto.ImgLink,
                TrainerSpecialization = trainerDto.TrainerSpecialization,
                Summary = trainerDto.Summary,
                CV = trainerDto.CV
            };

            _context.trainers.Add(trainer);
            await _context.SaveChangesAsync();

            trainerDto.Id = trainer.Id;

            return CreatedAtAction("GetTrainer", new { id = trainer.Id }, trainerDto);
        }

        // POST: api/Trainers/5/AddSubTrainings
        [HttpPost("{trainerId}/AddSubTrainings")]
        public async Task<ActionResult<TrainerDTO>> AddSubTrainingsToTrainer(int trainerId, [FromBody] List<int> subTrainingIds)
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

            var trainerDto = new TrainerDTO
            {
                Id = trainer.Id,
                TrainerName = trainer.TrainerName,
                ImgLink = trainer.ImgLink,
                ListSkills = trainer.ListSkills,
                TrainerSpecialization = trainer.TrainerSpecialization,
                Summary = trainer.Summary,
                CV = trainer.CV
            };

            return Ok(trainerDto);
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
