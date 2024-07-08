namespace Domain.Entities;

public class Receipt : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public string? Receiver { get; set; }
    public string? Payer { get; set; }
    public double Amount { get; set; }

}
