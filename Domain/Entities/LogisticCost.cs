namespace Domain.Entities;

public class LogisticCost : BaseAuditableEntity
{
    public string? Details { get; set; }
    public string? PhotoInvoiceURL { get; set; }

 
    public Center? Center { get; set; }
    public int CenterId { get; set; }
     public double TotalBalance { get; set; }
    public double Debt { get; set; }
   
    public List<Receipt>? Receipts { get; set; } = new List<Receipt>();
    public string? EmployeeName { get; set; }
}
