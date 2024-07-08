using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ClientAccount : BaseAuditableEntity
{
    public double Discount { get; set; }
    public double TotalBalance { get; set; }
    public double Debt { get; set; }

    public Client? Client { get; set; }
    public List<ReceiptFromClient>? ReceiptsFromClient { get; set; } = new List<ReceiptFromClient>();
    public List<TrainingInvoice>? TrainingInvoices { get; set; } = new List<TrainingInvoice>();
}
