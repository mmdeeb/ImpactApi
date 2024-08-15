namespace Impact.Api.Models;

public class HallAvailabilityDTO
{
    public int HallId { get; set; }
    public string HallName { get; set; }
    public List<DayAvailabilityDTO> DayAvailabilities { get; set; } = new List<DayAvailabilityDTO>();
}
