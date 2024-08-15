using Impact.Api.Models;
using ImpactApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Statistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableStatisticsDTO>>> GetStatistics()
        {
            var statistics = new List<TableStatisticsDTO>
            {
                new TableStatisticsDTO { TableName = "Ads", RecordCount = await _context.ads.CountAsync() },
                new TableStatisticsDTO { TableName = "Center", RecordCount = await _context.centers.CountAsync() },
                new TableStatisticsDTO { TableName = "Client", RecordCount = await _context.clients.CountAsync() },
                new TableStatisticsDTO { TableName = "Employee", RecordCount = await _context.employees.CountAsync() },
                new TableStatisticsDTO { TableName = "Hall", RecordCount = await _context.halls.CountAsync() },
                new TableStatisticsDTO { TableName = "Reservation", RecordCount = await _context.reservations.CountAsync() },
                new TableStatisticsDTO { TableName = "Restaurant", RecordCount = await _context.restaurants.CountAsync() },
                new TableStatisticsDTO { TableName = "SubTraining", RecordCount = await _context.subTrainings.CountAsync() },
                new TableStatisticsDTO { TableName = "Trainee", RecordCount = await _context.trainees.CountAsync() },
                new TableStatisticsDTO { TableName = "Trainer", RecordCount = await _context.trainers.CountAsync() },
                new TableStatisticsDTO { TableName = "Training", RecordCount = await _context.trainings.CountAsync() },
                new TableStatisticsDTO { TableName = "TrainingType", RecordCount = await _context.trainingTypes.CountAsync() },
                new TableStatisticsDTO { TableName = "User", RecordCount = await _context.Users.CountAsync() }
            };

            return Ok(statistics);
        }
    }
}

