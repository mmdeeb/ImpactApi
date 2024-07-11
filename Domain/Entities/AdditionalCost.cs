using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AdditionalCost : BaseAuditableEntity
{
    public double Cost { get; set; }
    public string? Detailes { get; set; }
    public DateTime Date { get; set; }
    public  string? PhotoInvoiceURL { get; set; }
    public TrainingInvoice? TrainingInvoice { get; set; }
    public int TrainingInvoiceId { get; set; }
}
