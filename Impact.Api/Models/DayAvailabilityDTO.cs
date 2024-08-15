namespace Impact.Api.Models;

public class DayAvailabilityDTO
{
    public DateTime Date { get; set; }
    public List<DateTime> ReservedSlots { get; set; } = new List<DateTime>();
    public List<DateTime> AvailableSlots { get; set; } = new List<DateTime>();
}

