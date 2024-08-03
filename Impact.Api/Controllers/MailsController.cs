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
    public class MailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MailDTO>>> GetMails()
        {
            var mails = await _context.mails.ToListAsync();

            var mailDtos = mails.Select(mail => new MailDTO
            {
                Id = mail.Id,
                MailName = mail.MailName,
                Number = mail.Number,
                MailPrice = mail.MailPrice,
                MailPriceForORG = mail.MailPriceForORG,
                TotalPrice = mail.TotalPrice,
                TotalPriceForORG = mail.TotalPriceForORG,
                RestaurantAccountId = mail.RestaurantAccountId,
                TrainingInvoiceId = mail.TrainingInvoiceId
            }).ToList();

            return Ok(mailDtos);
        }

        // GET: api/Mails/ByInvoice/5
        [HttpGet("ByInvoice/{trainingInvoiceId}")]
        public async Task<ActionResult<IEnumerable<MailDTO>>> GetMailsByInvoice(int trainingInvoiceId)
        {
            var mails = await _context.mails
                                      .Where(m => m.TrainingInvoiceId == trainingInvoiceId)
                                      .ToListAsync();

            if (!mails.Any())
            {
                return NotFound();
            }

            var mailDtos = mails.Select(mail => new MailDTO
            {
                Id = mail.Id,
                MailName = mail.MailName,
                Number = mail.Number,
                MailPrice = mail.MailPrice,
                MailPriceForORG = mail.MailPriceForORG,
                TotalPrice = mail.TotalPrice,
                TotalPriceForORG = mail.TotalPriceForORG,
                RestaurantAccountId = mail.RestaurantAccountId,
                TrainingInvoiceId = mail.TrainingInvoiceId
            }).ToList();

            return Ok(mailDtos);
        }

        // GET: api/Mails/ByRestaurantAccount/5
        [HttpGet("ByRestaurantAccount/{restaurantAccountId}")]
        public async Task<ActionResult<IEnumerable<MailDTO>>> GetMailsByRestaurantAccount(int restaurantAccountId)
        {
            var mails = await _context.mails
                                      .Where(m => m.RestaurantAccountId == restaurantAccountId)
                                      .ToListAsync();

            if (!mails.Any())
            {
                return NotFound();
            }

            var mailDtos = mails.Select(mail => new MailDTO
            {
                Id = mail.Id,
                MailName = mail.MailName,
                Number = mail.Number,
                MailPrice = mail.MailPrice,
                MailPriceForORG = mail.MailPriceForORG,
                TotalPrice = mail.TotalPrice,
                TotalPriceForORG = mail.TotalPriceForORG,
                RestaurantAccountId = mail.RestaurantAccountId,
                TrainingInvoiceId = mail.TrainingInvoiceId
            }).ToList();

            return Ok(mailDtos);
        }

        // GET: api/Mails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MailDTO>> GetMail(int id)
        {
            var mail = await _context.mails.FindAsync(id);

            if (mail == null)
            {
                return NotFound();
            }

            var mailDto = new MailDTO
            {
                Id = mail.Id,
                MailName = mail.MailName,
                Number = mail.Number,
                MailPrice = mail.MailPrice,
                MailPriceForORG = mail.MailPriceForORG,
                TotalPrice = mail.TotalPrice,
                TotalPriceForORG = mail.TotalPriceForORG,
                RestaurantAccountId = mail.RestaurantAccountId,
                TrainingInvoiceId = mail.TrainingInvoiceId
            };

            return Ok(mailDto);
        }

        // PUT: api/Mails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMail(int id, MailDTO mailDto)
        {
            if (id != mailDto.Id)
            {
                return BadRequest();
            }

            var mail = await _context.mails.FindAsync(id);
            if (mail == null)
            {
                return NotFound();
            }

            var previousTotalPriceForORG = mail.MailPriceForORG;
            var previousTotalPrice = mail.MailPrice;

            mail.MailName = mailDto.MailName;
            mail.Number = mailDto.Number;
            mail.MailPrice = mailDto.MailPrice;
            mail.MailPriceForORG = mailDto.MailPriceForORG;
            mail.TotalPrice =  mailDto.Number * mailDto.MailPrice;
            mail.TotalPriceForORG = mailDto.Number * mailDto.MailPriceForORG;
            mail.RestaurantAccountId = mailDto.RestaurantAccountId;
            mail.TrainingInvoiceId = mailDto.TrainingInvoiceId;

            _context.Entry(mail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var trainingInvoice = await _context.trainingInvoices.Include(ti => ti.ClientAccount).FirstOrDefaultAsync(ti => ti.Id == mailDto.TrainingInvoiceId);
                if (trainingInvoice != null)
                {
                    trainingInvoice.MealsCost -= previousTotalPriceForORG;
                    trainingInvoice.MealsCost += mail.TotalPriceForORG;
                    trainingInvoice.TotalCost -= previousTotalPriceForORG;
                    trainingInvoice.TotalCost += mail.TotalPriceForORG;

                    var clientAccount = trainingInvoice.ClientAccount;
                    if (clientAccount != null)
                    {
                        clientAccount.TotalBalance -= previousTotalPriceForORG;
                        clientAccount.TotalBalance += mail.TotalPriceForORG;
                        _context.Entry(clientAccount).State = EntityState.Modified;
                    }

                    _context.Entry(trainingInvoice).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                var restaurantAccount = await _context.restaurantAccounts.FindAsync(mailDto.RestaurantAccountId);
                if (restaurantAccount != null)
                {
                    restaurantAccount.TotalBalance -= previousTotalPrice;
                    restaurantAccount.TotalBalance += mail.TotalPrice;
                    _context.Entry(restaurantAccount).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailExists(id))
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

        // POST: api/Mails
        [HttpPost]
        public async Task<ActionResult<MailDTO>> PostMail(MailDTO mailDto)
        {
            var mail = new Mail
            {
                MailName = mailDto.MailName,
                Number = mailDto.Number,
                MailPrice = mailDto.MailPrice,
                MailPriceForORG = mailDto.MailPriceForORG,
                TotalPrice = mailDto.Number * mailDto.MailPrice,
                TotalPriceForORG = mailDto.Number * mailDto.MailPriceForORG,
                RestaurantAccountId = mailDto.RestaurantAccountId,
                TrainingInvoiceId = mailDto.TrainingInvoiceId
            };

            _context.mails.Add(mail);
            await _context.SaveChangesAsync();

            var trainingInvoice = await _context.trainingInvoices.Include(ti => ti.ClientAccount).FirstOrDefaultAsync(ti => ti.Id == mail.TrainingInvoiceId);
            if (trainingInvoice != null)
            {
                trainingInvoice.MealsCost += mail.TotalPriceForORG;
                trainingInvoice.TotalCost += mail.TotalPriceForORG;

                var clientAccount = trainingInvoice.ClientAccount;
                if (clientAccount != null)
                {
                    clientAccount.TotalBalance += mail.TotalPriceForORG;
                    _context.Entry(clientAccount).State = EntityState.Modified;
                }

                _context.Entry(trainingInvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            var restaurantAccount = await _context.restaurantAccounts.FindAsync(mail.RestaurantAccountId);
            if (restaurantAccount != null)
            {
                restaurantAccount.TotalBalance += mail.TotalPrice;
                _context.Entry(restaurantAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            mailDto.Id = mail.Id;

            return CreatedAtAction("GetMail", new { id = mail.Id }, mailDto);
        }

        // DELETE: api/Mails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMail(int id)
        {
            var mail = await _context.mails.FindAsync(id);
            if (mail == null)
            {
                return NotFound();
            }

            var totalPriceForORG = mail.TotalPriceForORG;
            var totalPrice = mail.TotalPrice;
            var trainingInvoice = await _context.trainingInvoices.Include(ti => ti.ClientAccount).FirstOrDefaultAsync(ti => ti.Id == mail.TrainingInvoiceId);

            _context.mails.Remove(mail);
            await _context.SaveChangesAsync();

            if (trainingInvoice != null)
            {
                trainingInvoice.MealsCost -= totalPriceForORG;
                trainingInvoice.TotalCost -= totalPriceForORG;

                var clientAccount = trainingInvoice.ClientAccount;
                if (clientAccount != null)
                {
                    clientAccount.TotalBalance -= totalPriceForORG;
                    _context.Entry(clientAccount).State = EntityState.Modified;
                }

                _context.Entry(trainingInvoice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            var restaurantAccount = await _context.restaurantAccounts.FindAsync(mail.RestaurantAccountId);
            if (restaurantAccount != null)
            {
                restaurantAccount.TotalBalance -= totalPrice;
                _context.Entry(restaurantAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool MailExists(int id)
        {
            return _context.mails.Any(e => e.Id == id);
        }
    }
}
