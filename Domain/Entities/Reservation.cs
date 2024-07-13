using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Reservation : BaseAuditableEntity
{
    public Hall? Hall { get; set;}
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Status { get; set; }
    public double Cost { get; set; }
  
    
    public Training? Training { get; set; } 
    public int? TrainingId { get; set; } 
}
