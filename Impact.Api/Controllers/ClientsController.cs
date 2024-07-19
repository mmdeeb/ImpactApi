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
using Microsoft.AspNetCore.Authorization;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            var clients = await _context.clients.ToListAsync();

            var clientDtos = clients.Select(client => new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                ClientAccountId = client.ClientAccountId
            }).ToList();

            return Ok(clientDtos);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClientDTO>> GetClient(string id)
        {
            var client = await _context.clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var clientDto = new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                ClientAccountId = client.ClientAccountId
            };

            return Ok(clientDto);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutClient(string id, ClientDTO clientDto)
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

            client.Name = clientDto.Name;
            client.PhoneNumber = clientDto.PhoneNumber;
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
        [Authorize]
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
                Name = clientDto.Name,
                Email = clientDto.Email,
                PhoneNumber = clientDto.PhoneNumber,
                ClientAccountId = clientAccount.Id
            };

            _context.clients.Add(client);
            await _context.SaveChangesAsync();

            clientDto.Id = client.Id;
            clientDto.ClientAccountId = client.ClientAccountId;

            return CreatedAtAction("GetClient", new { id = client.Id }, clientDto);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        [Authorize]
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

        private bool ClientExists(string id)
        {
            return _context.clients.Any(e => e.Id == id);
        }
    }
}
