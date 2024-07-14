using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Mail : BaseAuditableEntity
{
    public string? MailName { get; set; }
    public int Number { get; set;}
    public double MailPrice { get; set; }
    public double MailPriceForORG { get; set;}
    public double TotalPrice { get; set; }
    public double TotalPriceForORG { get; set; }
    public int RestaurantAccountId { get; set; }
    public RestaurantAccount? RestaurantAccount { get; set; }
    public int TrainingInvoiceId { get; set; }
    public TrainingInvoice? TrainingInvoice { get; set; }
    
}

