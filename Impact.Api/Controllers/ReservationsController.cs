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
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var reservations = await _context.reservations.ToListAsync();

            var reservationDtos = reservations.Select(reservation =>
            {
                if (reservation.StartTime > DateTime.Now)
                {
                    reservation.Status = "Scheduled";
                }
                else if (reservation.StartTime <= DateTime.Now && reservation.EndTime >= DateTime.Now)
                {
                    reservation.Status = "In Progress";
                }
                else
                {
                    reservation.Status = "Done";
                }

                return new ReservationDTO
                {
                    Id = reservation.Id,
                    HallId = reservation.HallId,
                    TrainingId = reservation.TrainingId.Value,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    Status = reservation.Status,
                    Cost = reservation.Cost
                };
            }).ToList();

            return Ok(reservationDtos);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            var reservation = await _context.reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.StartTime > DateTime.Now)
            {
                reservation.Status = "Scheduled";
            }
            else if (reservation.StartTime <= DateTime.Now && reservation.EndTime >= DateTime.Now)
            {
                reservation.Status = "In Progress";
            }
            else
            {
                reservation.Status = "Done";
            }
            var reservationDto = new ReservationDTO
            {
                Id = reservation.Id,
                HallId = reservation.HallId,
                TrainingId = reservation.TrainingId.Value,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                Status = reservation.Status,
                Cost = reservation.Cost
            };

            return Ok(reservationDto);
        }

        // GET: api/Reservations/ByHall/5
        [HttpGet("ByHall/{hallId}")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsByHall(int hallId)
        {
            var reservations = await _context.reservations
                                             .Where(r => r.HallId == hallId)
                                             .ToListAsync();

            if (!reservations.Any())
            {
                return NotFound();
            }

            var reservationDtos = reservations.Select(reservation =>
            {
                if (reservation.StartTime > DateTime.Now)
                {
                    reservation.Status = "Scheduled";
                }
                else if (reservation.StartTime <= DateTime.Now && reservation.EndTime >= DateTime.Now)
                {
                    reservation.Status = "In Progress";
                }
                else
                {
                    reservation.Status = "Done";
                }

                return new ReservationDTO
                {
                    Id = reservation.Id,
                    HallId = reservation.HallId,
                    TrainingId = reservation.TrainingId.Value,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    Status = reservation.Status,
                    Cost = reservation.Cost
                };
            }).ToList();

            return Ok(reservationDtos);
        }

        // GET: api/Reservations/ByDate
        [HttpGet("ByDate")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservationsByDate(DateTime date)
        {
            var reservations = await _context.reservations
                                             .Where(r => r.StartTime.Date == date.Date)
                                             .ToListAsync();

            if (!reservations.Any())
            {
                return NotFound();
            }

            var reservationDtos = reservations.Select(reservation =>
            {
                if (reservation.StartTime > DateTime.Now)
                {
                    reservation.Status = "Scheduled";
                }
                else if (reservation.StartTime <= DateTime.Now && reservation.EndTime >= DateTime.Now)
                {
                    reservation.Status = "In Progress";
                }
                else
                {
                    reservation.Status = "Done";
                }

                return new ReservationDTO
                {
                    Id = reservation.Id,
                    HallId = reservation.HallId,
                    TrainingId = reservation.TrainingId.Value,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    Status = reservation.Status,
                    Cost = reservation.Cost
                };
            }).ToList();

            return Ok(reservationDtos);
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservationDto)
        {
            if (id != reservationDto.Id)
            {
                return BadRequest();
            }
           

            var reservation = await _context.reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var conflictingReservations = await _context.reservations
                                                       .Where(r => r.HallId == reservationDto.HallId &&
                                                                   r.Id != id &&
                                                                   r.StartTime < reservationDto.EndTime &&
                                                                   r.EndTime > reservationDto.StartTime)
                                                       .ToListAsync();

            if (conflictingReservations.Any())
            {
                return Conflict(new { Message = "There is a conflicting reservation for the given time period." });
            }

            var previousCost = reservation.Cost;

            reservation.HallId = reservationDto.HallId;
            reservation.TrainingId= reservationDto.TrainingId;
            reservation.StartTime = reservationDto.StartTime;
            reservation.EndTime = reservationDto.EndTime;
            if (reservation.StartTime > DateTime.Now)
            {
                reservation.Status = "Scheduled";
            }
            else if (reservation.StartTime <= DateTime.Now && reservation.EndTime >= DateTime.Now)
            {
                reservation.Status = "In Progress";
            }
            else
            {
                reservation.Status = "Done";
            }
            reservation.Cost = reservationDto.Cost;

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var training = await _context.trainings.Include(t => t.TrainingInvoice).FirstOrDefaultAsync(t => t.Id == reservationDto.TrainingId);
                var trainingInvoice = training?.TrainingInvoice; 
                if (trainingInvoice != null)
                {
                    trainingInvoice.ReservationsCost -= previousCost;
                    trainingInvoice.TotalCost -= previousCost;
                    trainingInvoice.ReservationsCost += reservation.Cost;
                    trainingInvoice.TotalCost += reservation.Cost;
                    var ClientAccount = await _context.clientAccounts.FirstOrDefaultAsync(ca => ca.Id == trainingInvoice.ClientAccountId);
                    if (ClientAccount != null)
                    {
                        ClientAccount.TotalBalance -= previousCost;
                        ClientAccount.TotalBalance += reservation.Cost;
                        _context.Entry(ClientAccount).State = EntityState.Modified;

                    }

                    _context.Entry(trainingInvoice).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDto)
        {
            var conflictingReservations = await _context.reservations
                                                        .Where(r => r.HallId == reservationDto.HallId &&
                                                                    r.StartTime < reservationDto.EndTime &&
                                                                    r.EndTime > reservationDto.StartTime)
                                                        .ToListAsync();

            if (conflictingReservations.Any())
            {
                return Conflict(new { Message = "There is a conflicting reservation for the given time period." });
            }

            reservationDto.Status = "Scheduled";

            var reservation = new Reservation
            {
                HallId = reservationDto.HallId,
                TrainingId = reservationDto.TrainingId,
                StartTime = reservationDto.StartTime,
                EndTime = reservationDto.EndTime,
                Status = reservationDto.Status,
                Cost = reservationDto.Cost
            };

            _context.reservations.Add(reservation);
            await _context.SaveChangesAsync();
            var training = await _context.trainings.Include(t => t.TrainingInvoice).FirstOrDefaultAsync(t => t.Id == reservationDto.TrainingId);
            var trainingInvoice = training?.TrainingInvoice; 
            if (trainingInvoice != null)
            {
                trainingInvoice.ReservationsCost += reservation.Cost;
                trainingInvoice.TotalCost += reservation.Cost;
                var ClientAccount = await _context.clientAccounts.FirstOrDefaultAsync(ca => ca.Id == trainingInvoice.ClientAccountId);
                if (ClientAccount != null)
                {
                    ClientAccount.TotalBalance += reservation.Cost;
                    _context.Entry(ClientAccount).State = EntityState.Modified;

                }
                _context.Entry(trainingInvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            reservationDto.Id = reservation.Id;

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservationDto);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.reservations
                .Include(r => r.Training)
                    .ThenInclude(t => t.TrainingInvoice)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            var cost = reservation.Cost;
            var training = await _context.trainings.Include(t => t.TrainingInvoice).FirstOrDefaultAsync(t => t.Id == reservation.TrainingId);
            var trainingInvoice = training?.TrainingInvoice;
            _context.reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            if (trainingInvoice != null)
            {
                trainingInvoice.ReservationsCost -= cost;
                trainingInvoice.TotalCost -= cost;
                var ClientAccount = await _context.clientAccounts.FirstOrDefaultAsync(ca => ca.Id == trainingInvoice.ClientAccountId);
                if (ClientAccount != null)
                {
                    ClientAccount.TotalBalance -= cost;
                    _context.Entry(ClientAccount).State = EntityState.Modified;

                }
                _context.Entry(trainingInvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        // PATCH: api/Reservations/UpdateStatus/5
        [HttpPatch("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateReservationStatus(int id)
        {
            var reservation = await _context.reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.StartTime > DateTime.Now)
            {
                reservation.Status = "Scheduled";
            }
            else if (reservation.StartTime <= DateTime.Now && reservation.EndTime >= DateTime.Now)
            {
                reservation.Status = "In Progress";
            }
            else
            {
                reservation.Status = "Done";
            }

            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.reservations.Any(e => e.Id == id);
        }
    }
}
