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
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.employees.ToListAsync();

            var employeeDtos = employees.Select(employee => new EmployeeDTO
            {
                Id = employee.Id,
                UserId = employee.UserId,
                EmployeeType = employee.EmployeeType,
                Salary = employee.Salary,
                CenterId = employee.CenterId,
                EmployeeAccountId = employee.EmployeeAccountId
            }).ToList();

            return Ok(employeeDtos);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDTO
            {
               
                Id = employee.Id,
                UserId = employee.UserId,
                EmployeeType = employee.EmployeeType,
                Salary = employee.Salary,
                CenterId = employee.CenterId,
                EmployeeAccountId = employee.EmployeeAccountId
            };

            return Ok(employeeDto);
        }

        // GET: api/Employees/ByCenter/5
        [HttpGet("ByCenter/{centerId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesByCenter(int centerId)
        {
            var employees = await _context.employees
                                          .Where(e => e.CenterId == centerId)
                                          .ToListAsync();

            if (!employees.Any())
            {
                return NotFound();
            }

            var employeeDtos = employees.Select(employee => new EmployeeDTO
            {
                Id = employee.Id,
                UserId = employee.UserId,
                EmployeeType = employee.EmployeeType,
                Salary = employee.Salary,
                CenterId = employee.CenterId,
                EmployeeAccountId = employee.EmployeeAccountId
            }).ToList();

            return Ok(employeeDtos);
        }

        // GET: api/Clients/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeByUserId(Guid userId)
        {
            var employee = await _context.employees.FirstOrDefaultAsync(e => e.UserId == userId);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDTO
            {
                Id = employee.Id,
                UserId = employee.UserId,
                EmployeeType = employee.EmployeeType,
                Salary = employee.Salary,
                CenterId = employee.CenterId,
                EmployeeAccountId = employee.EmployeeAccountId
            };

            return Ok(employeeDto);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest();
            }

            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.UserId = employeeDto.UserId;
            employee.EmployeeType = employeeDto.EmployeeType;
            employee.Salary = employeeDto.Salary;
            employee.CenterId = employeeDto.CenterId;
            employee.EmployeeAccountId = employeeDto.EmployeeAccountId;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDto)
        {
            var employeeAccount = new EmployeeAccount
            {
                TotalBalance = 0,
                Debt = 0
            };

            _context.employeeAccounts.Add(employeeAccount);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                UserId = employeeDto.UserId,
                EmployeeType = employeeDto.EmployeeType,
                Salary = employeeDto.Salary,
                CenterId = employeeDto.CenterId,
                EmployeeAccountId = employeeAccount.Id
            };

            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            employeeDto.Id = employee.Id;
            employeeDto.UserId = employeeDto.UserId;
            employeeDto.EmployeeAccountId = employeeAccount.Id;

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employeeDto);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeAccount = await _context.employeeAccounts.FindAsync(employee.EmployeeAccountId);
            if (employeeAccount != null)
            {
                _context.employeeAccounts.Remove(employeeAccount);
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Employees/UpdateSalary/5
        [HttpPatch("UpdateSalary/{id}")]
        public async Task<IActionResult> UpdateEmployeeSalary(int id, [FromBody] double newSalary)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Salary = newSalary;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Employees/AddSalaryToAccount/5
        [HttpPost("AddSalaryToAccount/{id}")]
        public async Task<IActionResult> AddSalaryToEmployeeAccount(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeAccount = await _context.employeeAccounts.FindAsync(employee.EmployeeAccountId);
            if (employeeAccount == null)
            {
                return NotFound();
            }

            employeeAccount.Debt += employee.Salary;

            _context.Entry(employeeAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
