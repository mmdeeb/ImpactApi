namespace Domain.Entities;

public class OtherExpenses : BaseAuditableEntity
{
    public string? Description { get; set; }
    public string? PhotoInvoiceURL { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
   
    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; }
 }
