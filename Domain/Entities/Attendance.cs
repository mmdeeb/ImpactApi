using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Attendance : BaseAuditableEntity
{
    public DateTime AttendanceName { get; set;}
    public Training? Training { get; set; }
    public int TrainingId { get; set; }
    public List<Trainee>? Trainee { get; set; } = new List<Trainee>(); 
}
