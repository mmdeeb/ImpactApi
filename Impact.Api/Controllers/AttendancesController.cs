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
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDTO>>> GetAttendances()
        {
            var attendances = await _context.attendances.Include(a => a.Training).ToListAsync();

            var attendanceDtos = attendances.Select(attendance => new AttendanceDTO
            {
                Id = attendance.Id,
                AttendanceDate = attendance.AttendanceDate,
                TrainingId = attendance.TrainingId,
                TrainingName = attendance.Training?.TrainingName
            }).ToList();

            return Ok(attendanceDtos);
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDTO>> GetAttendance(int id)
        {
            var attendance = await _context.attendances.FirstOrDefaultAsync(a => a.Id == id);

            if (attendance == null)
            {
                return NotFound();
            }

            var attendanceDto = new AttendanceDTO
            {
                Id = attendance.Id,
                AttendanceDate = attendance.AttendanceDate,
                TrainingId = attendance.TrainingId,
                TrainingName = attendance.Training?.TrainingName
            };

            return Ok(attendanceDto);
        }

        // GET: api/Attendances/Trainees/5
        [HttpGet("Trainees/{attendanceId}")]
        public async Task<ActionResult<IEnumerable<TraineeDTO>>> GetTraineesByAttendance(int attendanceId)
        {
            var attendance = await _context.attendances
                                           .Include(a => a.Trainee)
                                           .FirstOrDefaultAsync(a => a.Id == attendanceId);

            if (attendance == null)
            {
                return NotFound();
            }

            var traineeDtos = attendance.Trainee?.Select(t => new TraineeDTO
            {
                Id = t.Id,
                TraineeName = t.TraineeName,
                ListAttendanceStatus = t.ListAttendanceStatus,
                TrainingId = t.TrainingId,
            }).ToList();

            return Ok(traineeDtos);
        }

        // PUT: api/Attendances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, AttendanceDTO attendanceDto)
        {
            if (id != attendanceDto.Id)
            {
                return BadRequest();
            }
            
            var attendance = await _context.attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            attendance.AttendanceDate = attendanceDto.AttendanceDate;
            attendance.TrainingId = attendanceDto.TrainingId;

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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

        // POST: api/Attendances
        [HttpPost]
        public async Task<ActionResult<AttendanceDTO>> PostAttendance(AttendanceDTO attendanceDto)
        {
            var attendance = new Attendance
            {
                AttendanceDate = attendanceDto.AttendanceDate,
                TrainingId = attendanceDto.TrainingId,
            };

            _context.attendances.Add(attendance);
            await _context.SaveChangesAsync();

            attendanceDto.Id = attendance.Id;

            return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendanceDto);
        }

        // POST: api/Attendances/5/AddTrainees
        [HttpPost("{id}/AddTrainees")]
        public async Task<IActionResult> AddTraineesToAttendance(int id, [FromBody] List<int> traineeIds)
        {
            var attendance = await _context.attendances
                                           .Include(a => a.Trainee)
                                           .FirstOrDefaultAsync(a => a.Id == id);

            if (attendance == null)
            {
                return NotFound(new { Message = "Attendance not found" });
            }

            var trainees = await _context.trainees
                                         .Where(t => traineeIds.Contains(t.Id))
                                         .ToListAsync();

            if (trainees == null || !trainees.Any())
            {
                return NotFound(new { Message = "One or more trainees not found" });
            }

            if (attendance.Trainee == null)
            {
                attendance.Trainee = new List<Trainee>();
            }

            foreach (var trainee in trainees)
            {
                if (!attendance.Trainee.Contains(trainee))
                {
                    attendance.Trainee.Add(trainee);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _context.attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceExists(int id)
        {
            return _context.attendances.Any(e => e.Id == id);
        }
    }
}
