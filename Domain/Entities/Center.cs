namespace Domain.Entities;

public class Center : BaseAuditableEntity
{
    public string? CenterName { get; set; }
    public string? CenterLocation { get; set;}
    public string? PhoneNumber { get; set;}
    public List<Hall>? Halls { get; set; } = new List<Hall>(); // المركز فيه عدد من القاعات
    public string? Media { get; set; }
    public List<Employee>? Employees { get; set; } = new List<Employee>(); // المركز فيه عدد من الموظفين
}

