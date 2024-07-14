using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class TrainingInvoice : BaseAuditableEntity
{
    public double MealsCost { get; set; }
    public double TrainerCost { get; set; }
    public string? PhotoInvoiceURL { get; set; }
    public double ReservationsCost { get; set; }
    public double AllAdditionalCosts { get; set; }
    public double TotalCost { get; set; }
    public double Discount { get; set; }
    public double FinalCost { get; set; }
 
    public Training? Traning { get; set; }
    public ClientAccount? ClientAccount { get; set; }
    public int? ClientAccountId { get; set; }
      
    public List<AdditionalCost>? AdditionalCosts { get; set; } = new List<AdditionalCost>();
    public List<Mail>? Meals { get; set; } = new List<Mail>();
}
