using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Impact.Api.Models;
using ImpactApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingTypeDTO>>> GetTrainingTypes()
        {
            var trainingTypes = await _context.trainingTypes.ToListAsync();
           var trainingTypeDTOs = trainingTypes.Select(tt => new TrainingTypeDTO
            {
                Id = tt.Id,
                TrainingTypeName = tt.TrainingTypeName,
                ImgLink = tt.ImgLink,
            }).ToList();

            return trainingTypeDTOs;
        }

        // GET: api/TrainingTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingTypeDTO>> GetTrainingType(int id)
        {
            var trainingType = await _context.trainingTypes.FindAsync(id);

            if (trainingType == null)
            {
                return NotFound();
            }

            var trainingTypeDTO = new TrainingTypeDTO
            {
                Id = trainingType.Id,
                TrainingTypeName = trainingType.TrainingTypeName,
                ImgLink = trainingType.ImgLink,
            };

            return trainingTypeDTO;
        }

        // PUT: api/TrainingTypes/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTrainingType(int id, TrainingTypeDTO trainingTypeDTO)
        {
            if (id != trainingTypeDTO.Id)
            {
                return BadRequest();
            }

            var trainingType = await _context.trainingTypes.FindAsync(id);
            if (trainingType == null)
            {
                return NotFound();
            }

            trainingType.TrainingTypeName = trainingTypeDTO.TrainingTypeName;
            trainingType.ImgLink = trainingTypeDTO.ImgLink;

            _context.Entry(trainingType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingTypeExists(id))
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

        // POST: api/TrainingTypes
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TrainingTypeDTO>> PostTrainingType(TrainingTypeDTO trainingTypeDTO)
        {
            var trainingType = new TrainingType
            {
                TrainingTypeName = trainingTypeDTO.TrainingTypeName,
                ImgLink = trainingTypeDTO.ImgLink,
            };

            _context.trainingTypes.Add(trainingType);
            await _context.SaveChangesAsync();

            trainingTypeDTO.Id = trainingType.Id;

            return CreatedAtAction(nameof(GetTrainingType), new { id = trainingTypeDTO.Id }, trainingTypeDTO);
        }

        // DELETE: api/TrainingTypes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTrainingType(int id)
        {
            var trainingType = await _context.trainingTypes.FindAsync(id);
            if (trainingType == null)
            {
                return NotFound();
            }

            _context.trainingTypes.Remove(trainingType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingTypeExists(int id)
        {
            return _context.trainingTypes.Any(e => e.Id == id);
        }
    }
}
