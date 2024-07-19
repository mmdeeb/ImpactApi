using Microsoft.AspNetCore.Identity;

namespace Impact.Api.Models;

public class EmployeeDTO
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmployeeType { get; set; }
    public double Salary { get; set; }
    public int CenterId { get; set; }
    public int EmployeeAccountId { get; set; }
}
