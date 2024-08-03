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
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdsDTO>>> GetAds()
        {
            var adsList = await _context.ads.ToListAsync();

            var adsDtos = adsList.Select(ad => new AdsDTO
            {
                Id = ad.Id,
                ListAdsMedia = ad.ListAdsMedia,
                AdsTitle = ad.AdsTitle,
                AdsDescription = ad.AdsDescription,
                AdsLink = ad.AdsLink
            }).ToList();

            return Ok(adsDtos);
        }

        // GET: api/Ads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdsDTO>> GetAds(int id)
        {
            var ads = await _context.ads.FindAsync(id);

            if (ads == null)
            {
                return NotFound();
            }

            var adsDto = new AdsDTO
            {
                Id = ads.Id,
                ListAdsMedia = ads.ListAdsMedia,
                AdsTitle = ads.AdsTitle,
                AdsDescription = ads.AdsDescription,
                AdsLink = ads.AdsLink
            };

            return Ok(adsDto);
        }

        // PUT: api/Ads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAds(int id, AdsDTO adsDto)
        {
            if (id != adsDto.Id)
            {
                return BadRequest();
            }

            var ads = await _context.ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }

            ads.ListAdsMedia = adsDto.ListAdsMedia;
            ads.AdsTitle = adsDto.AdsTitle;
            ads.AdsDescription = adsDto.AdsDescription;
            ads.AdsLink = adsDto.AdsLink;

            _context.Entry(ads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdsExists(id))
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

        // POST: api/Ads
        [HttpPost]
        public async Task<ActionResult<AdsDTO>> PostAds(AdsDTO adsDto)
        {
            var ads = new Ads
            {
                ListAdsMedia = adsDto.ListAdsMedia,
                AdsTitle = adsDto.AdsTitle,
                AdsDescription = adsDto.AdsDescription,
                AdsLink = adsDto.AdsLink
            };

            _context.ads.Add(ads);
            await _context.SaveChangesAsync();

            adsDto.Id = ads.Id;

            return CreatedAtAction("GetAds", new { id = ads.Id }, adsDto);
        }

        // DELETE: api/Ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAds(int id)
        {
            var ads = await _context.ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }

            _context.ads.Remove(ads);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdsExists(int id)
        {
            return _context.ads.Any(e => e.Id == id);
        }
    }
}
