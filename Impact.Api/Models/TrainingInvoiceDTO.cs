namespace Impact.Api.Models;

public class TrainingInvoiceDTO
{
    public int Id { get; set; }
    public double MealsCost { get; set; }
    public double TrainerCost { get; set; }
    public string? PhotoInvoiceURL { get; set; }
    public double ReservationsCost { get; set; }
    public double AllAdditionalCosts { get; set; }
    public double TotalCost { get; set; }
    public double Discount { get; set; }
    public double FinalCost { get; set; }
    public int? ClientAccountId { get; set; }
}
