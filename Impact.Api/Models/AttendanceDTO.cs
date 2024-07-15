namespace Impact.Api.Models;

public class AttendanceDTO
{
    public int Id { get; set; }
    public DateTime AttendanceDate { get; set; }
    public int TrainingId { get; set; }
    public string? TrainingName { get; set; }
}
