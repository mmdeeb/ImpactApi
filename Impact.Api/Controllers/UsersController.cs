using Domain.Entities;
using Impact.Api.Models;
using ImpactApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetUsers()
        {


            var users = _userManager.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            }).ToList();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return Ok(userDto);
        }

        // PUT: api/Users/5
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUser(string email, UpdateUserDTO updateUserDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUserDto.Name;
            user.UserName = updateUserDto.UserName;
            user.PhoneNumber = updateUserDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

    

            return NoContent();
        }

    }
    public class UpdateUserDTO
    {
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
    }
}
