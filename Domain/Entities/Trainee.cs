using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Trainee : BaseAuditableEntity
{
    public string? TraineeName { get; set; }
    public string? ListAttendanceStatus { get; set; }
    public Training? Training { get; set; } 
    [ForeignKey("Training")]
    public int TrainingId { get; set; }
}
