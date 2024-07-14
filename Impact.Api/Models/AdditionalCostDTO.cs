namespace Impact.Api.Models;

public class AdditionalCostDTO
{
    public int Id { get; set; }
    public double Cost { get; set; }
    public string? Detailes { get; set; }
    public DateTime Date { get; set; }
    public string? PhotoInvoiceURL { get; set; }
    public int TrainingInvoiceId { get; set; }
}
