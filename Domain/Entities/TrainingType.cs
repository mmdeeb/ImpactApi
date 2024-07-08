namespace Domain.Entities; 

public class TrainingType : BaseAuditableEntity
{
    public string? TrainingTypeName { get; set; }
    public List<SubTraining>? SubTraining { get; set; } = new List<SubTraining>();
}
