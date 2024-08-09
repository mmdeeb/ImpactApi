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
    public class SubTrainingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubTrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubTrainings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTrainingDTO>>> GetSubTrainings()
        {
            var subTrainings = await _context.subTrainings.ToListAsync();

            var subTrainingDtos = subTrainings.Select(st => new SubTrainingDTO
            {
                Id = st.Id,
                SubTrainingName = st.SubTrainingName,
                ImgLink = st.ImgLink,
                SubTrainingDescription = st.SubTrainingDescription,
                TrainingTypeId = st.TrainingTypeId
            }).ToList();

            return Ok(subTrainingDtos);
        }

        // GET: api/SubTrainings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubTrainingDTO>> GetSubTraining(int id)
        {
            var subTraining = await _context.subTrainings.FirstOrDefaultAsync(st => st.Id == id);

            if (subTraining == null)
            {
                return NotFound();
            }

            var subTrainingDto = new SubTrainingDTO
            {
                Id = subTraining.Id,
                SubTrainingName = subTraining.SubTrainingName,
                ImgLink = subTraining.ImgLink,
                SubTrainingDescription = subTraining.SubTrainingDescription,
                TrainingTypeId = subTraining.TrainingTypeId
            };

            return Ok(subTrainingDto);
        }

        // GET: api/SubTrainings/GetSubTrainingsByTrainer/5
        [HttpGet("GetSubTrainingsByTrainer/{trainerId}")]
        public async Task<ActionResult<IEnumerable<SubTrainingDTO>>> GetSubTrainingsByTrainer(int trainerId)
        {
            var subTrainings = await _context.trainers
                                             .Where(t => t.Id == trainerId)
                                             .SelectMany(t => t.SubTraining)
                                             .Include(st => st.Trainers)
                                             .ToListAsync();

            if (subTrainings == null || !subTrainings.Any())
            {
                return NotFound();
            }

            var subTrainingDtos = subTrainings.Select(st => new SubTrainingDTO
            {
                Id = st.Id,
                SubTrainingName = st.SubTrainingName,
                ImgLink = st.ImgLink,
                SubTrainingDescription = st.SubTrainingDescription,
                TrainingTypeId = st.TrainingTypeId
            }).ToList();

            return Ok(subTrainingDtos);
        }

        // PUT: api/SubTrainings/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutSubTraining(int id, SubTrainingDTO subTrainingDto)
        {
            if (id != subTrainingDto.Id)
            {
                return BadRequest();
            }

            var subTraining = await _context.subTrainings.FindAsync(id);
            if (subTraining == null)
            {
                return NotFound();
            }

            subTraining.SubTrainingName = subTrainingDto.SubTrainingName;
            subTraining.ImgLink = subTrainingDto.ImgLink;
            subTraining.SubTrainingDescription = subTrainingDto.SubTrainingDescription;
            subTraining.TrainingTypeId = subTrainingDto.TrainingTypeId;

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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SubTrainingDTO>> PostSubTraining(SubTrainingDTO subTrainingDto)
        {
            var subTraining = new SubTraining
            {
                SubTrainingName = subTrainingDto.SubTrainingName,
                ImgLink = subTrainingDto.ImgLink,
                SubTrainingDescription = subTrainingDto.SubTrainingDescription,
                TrainingTypeId = subTrainingDto.TrainingTypeId
            };

            _context.subTrainings.Add(subTraining);
            await _context.SaveChangesAsync();

            subTrainingDto.Id = subTraining.Id;

            return CreatedAtAction("GetSubTraining", new { id = subTraining.Id }, subTrainingDto);
        }

        // POST: api/SubTrainings/5/AddTrainers
        [HttpPost("{subTrainingId}/AddTrainers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SubTrainingDTO>> AddTrainersToSubTraining(int subTrainingId, [FromBody] List<int> trainerIds)
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

            var subTrainingDto = new SubTrainingDTO
            {
                Id = subTraining.Id,
                SubTrainingName = subTraining.SubTrainingName,
                ImgLink = subTraining.ImgLink,
                SubTrainingDescription = subTraining.SubTrainingDescription,
                TrainingTypeId = subTraining.TrainingTypeId
            };

            return Ok(subTrainingDto);
        }

        // DELETE: api/SubTrainings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
