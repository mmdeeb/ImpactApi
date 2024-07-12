namespace Impact.Api.Models;

public class TrainingDTO
{
    public int Id { get; set; }
    public string? TrainingName { get; set; }
    public int NumberOfStudents { get; set; }
    public string? TrainingDetails { get; set; }
    public int TrainingInvoiceId { get; set; }
    public int ClientId { get; set; }
}
