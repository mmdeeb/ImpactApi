namespace Impact.Api.Models;

public class ReceiptFromClientDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Receiver { get; set; }
    public string? Payer { get; set; }
    public double Amount { get; set; }
    public int ClientAccountId { get; set; }
}
