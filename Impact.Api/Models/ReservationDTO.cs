namespace Impact.Api.Models;

public class ReservationDTO
{
    public int Id { get; set; }
    public int HallId { get; set; }
    public int TrainingId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Status { get; set; }
    public double Cost { get; set; }
}
