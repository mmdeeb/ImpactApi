namespace Impact.Api.Models;

public class MailDTO
{
    public int Id { get; set; }
    public string? MailName { get; set; }
    public int Number { get; set; }
    public double MailPrice { get; set; }
    public double MailPriceForORG { get; set; }
    public double TotalPrice { get; set; }
    public double TotalPriceForORG { get; set; }
    public int RestaurantAccountId { get; set; }
    public int TrainingInvoiceId { get; set; }
}
