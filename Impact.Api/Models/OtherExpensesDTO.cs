namespace Impact.Api.Models;

public class OtherExpensesDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? PhotoInvoiceURL { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string? EmployeeName { get; set; }
    public int CenterId { get; set; }
}
