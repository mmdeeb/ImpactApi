using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Reservation : BaseAuditableEntity
{
   // Reservation حجز
    public Hall? Hall { get; set;} // القاعة الخاصة بالحجز
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Status { get; set; }
    public double Cost { get; set; }
  
    
    public Training? Training { get; set; } //  تدريب خاص بالحجز الواحد
   // public int TrainingId { get; set; }
}
