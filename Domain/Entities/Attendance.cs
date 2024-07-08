namespace Domain.Entities;

public class Attendance : BaseAuditableEntity
{
    public DateTime AttendanceName { get; set;}
    public List<Trainee>? Trainee { get; set; } = new List<Trainee>(); // في الحصة يحضر اكثر من متدرب
}
