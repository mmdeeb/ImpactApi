using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ImpactApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Impact.Api.Models;

namespace Impact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialFundsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinancialFundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FinancialFunds
        [HttpGet]
        public async Task<ActionResult<FinancialFundDTO>> GetFinancialFund()
        {
            var totalRestaurantDebt = await _context.restaurantAccounts.SumAsync(r => r.Debt);
            var totalEmployeeDebt = await _context.employeeAccounts.SumAsync(e => e.Debt);
            var totalClientDebt = await _context.clientAccounts.SumAsync(c => c.Debt);


            var financialFundDto = new FinancialFundDTO
            {
                DebtOnTheFund = totalRestaurantDebt + totalEmployeeDebt,
                DebtToTheFund = totalClientDebt
            };

            return Ok(financialFundDto);
        }

             
    }
}
