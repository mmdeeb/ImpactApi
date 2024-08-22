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
using Microsoft.AspNetCore.Identity;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ClientsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        // GET: api/Clients
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            var clients = await _context.clients.ToListAsync();

            var clientDtos = new List<ClientDTO>();

            foreach (var client in clients)
            {
                var user = await _userManager.FindByIdAsync(client.UserId.ToString());

                if (user != null)
                {
                    var clientDto = new ClientDTO
                    {
                        Id = client.Id,
                        UserId = client.UserId,
                        ClientAccountId = client.ClientAccountId,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    };

                    clientDtos.Add(clientDto);
                }
            }

            return Ok(clientDtos);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {
            var client = await _context.clients.FindAsync(id);
            var user = await _userManager.FindByIdAsync(client.UserId.ToString());

            if (client == null)
            {
                return NotFound();
            }

            var clientDto = new ClientDTO
            {
                Id = client.Id,
                UserId = client.UserId,
                ClientAccountId = client.ClientAccountId,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(clientDto);
        }

        // GET: api/Clients/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        [Authorize]
        public async Task<ActionResult<ClientDTO>> GetClientByUserId(Guid userId)
        {
            var client = await _context.clients.FirstOrDefaultAsync(c => c.UserId == userId);

            if (client == null)
            {
                return NotFound();
            }

            var clientDto = new ClientDTO
            {
                Id = client.Id,
                UserId = client.UserId,
                ClientAccountId = client.ClientAccountId
            };

            return Ok(clientDto);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClient(int id, ClientDTO clientDto)
        {
            if (id != clientDto.Id)
            {
                return BadRequest();
            }

            var client = await _context.clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            client.UserId = client.UserId;
            client.ClientAccountId = clientDto.ClientAccountId;

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClientDTO>> PostClient(ClientDTO clientDto)
        {
            var clientAccount = new ClientAccount
            {
                Discount = 0,
                TotalBalance = 0,
                Debt = 0
            };

            _context.clientAccounts.Add(clientAccount);
            await _context.SaveChangesAsync();

            var client = new Client
            {
                UserId = clientDto.UserId,
                ClientAccountId = clientAccount.Id
            };

            _context.clients.Add(client);
            await _context.SaveChangesAsync();

            clientDto.Id = client.Id;
            clientDto.UserId = client.UserId;
            clientDto.ClientAccountId = client.ClientAccountId;

            return CreatedAtAction("GetClient", new { id = client.Id }, clientDto);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            var client = await _context.clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.clients.Any(e => e.Id == id);
        }
    }
}
