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
    public class TraineesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TraineesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Trainees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraineeDTO>>> GetTrainees()
        {
            var trainees = await _context.trainees.ToListAsync();

            var traineeDtos = trainees.Select(trainee => new TraineeDTO
            {
                Id = trainee.Id,
                TraineeName = trainee.TraineeName,
                ListAttendanceStatus = trainee.ListAttendanceStatus,
                TrainingId = trainee.TrainingId,
            }).ToList();

            return Ok(traineeDtos);
        }

        // GET: api/Trainees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraineeDTO>> GetTrainee(int id)
        {
            var trainee = await _context.trainees.FirstOrDefaultAsync(t => t.Id == id);

            if (trainee == null)
            {
                return NotFound();
            }

            var traineeDto = new TraineeDTO
            {
                Id = trainee.Id,
                TraineeName = trainee.TraineeName,
                ListAttendanceStatus = trainee.ListAttendanceStatus,
                TrainingId = trainee.TrainingId,
            };

            return Ok(traineeDto);
        }

        // GET: api/Trainees/ByTraining/5
        [HttpGet("ByTraining/{trainingId}")]
        public async Task<ActionResult<IEnumerable<TraineeDTO>>> GetTraineesByTraining(int trainingId)
        {
            var trainees = await _context.trainees
                                         .Where(t => t.TrainingId == trainingId)
                                         .ToListAsync();

            if (trainees == null || !trainees.Any())
            {
                return NotFound();
            }

            var traineeDtos = trainees.Select(trainee => new TraineeDTO
            {
                Id = trainee.Id,
                TraineeName = trainee.TraineeName,
                ListAttendanceStatus = trainee.ListAttendanceStatus,
                TrainingId = trainee.TrainingId,
            }).ToList();

            return Ok(traineeDtos);
        }

        // PUT: api/Trainees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainee(int id, TraineeDTO traineeDto)
        {
            if (id != traineeDto.Id)
            {
                return BadRequest();
            }

            var trainee = await _context.trainees.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }

            trainee.TraineeName = traineeDto.TraineeName;
            trainee.ListAttendanceStatus = traineeDto.ListAttendanceStatus;
            trainee.TrainingId = traineeDto.TrainingId;

            _context.Entry(trainee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeExists(id))
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

        // POST: api/Trainees
        [HttpPost]
        public async Task<ActionResult<TraineeDTO>> PostTrainee(TraineeDTO traineeDto)
        {
            var trainee = new Trainee
            {
                TraineeName = traineeDto.TraineeName,
                ListAttendanceStatus = traineeDto.ListAttendanceStatus,
                TrainingId = traineeDto.TrainingId
            };

            _context.trainees.Add(trainee);
            await _context.SaveChangesAsync();

            traineeDto.Id = trainee.Id;

            return CreatedAtAction("GetTrainee", new { id = trainee.Id }, traineeDto);
        }

        // DELETE: api/Trainees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainee(int id)
        {
            var trainee = await _context.trainees.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }

            _context.trainees.Remove(trainee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraineeExists(int id)
        {
            return _context.trainees.Any(e => e.Id == id);
        }
    }
}
