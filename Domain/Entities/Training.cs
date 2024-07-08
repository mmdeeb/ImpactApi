using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Entities;

public class Training : BaseAuditableEntity
{
    public string? TrainingName { get; set; }
    public int NumberOfStudents { get; set; }
    public string? TrainingDetails { get; set;}
 
    public List<AdditionalCost>? AdditionalCosts { get; set; } = new List<AdditionalCost>(); //  تكاليف اضافية للتدريب
   
    public List<Attendance>? Attendances { get; set; } = new List<Attendance>(); // سجلات الحضور للتدريب
 
    public int TrainingInvoiceId { get; set; } 
    public TrainingInvoice? TrainingInvoice { get; set; } // فاتورة التدريب
  
    public List<Trainee>? Trainees { get; set; } = new List<Trainee>(); //  مجموعة متدربين للتدريب
  
    public List<Reservation>? Reservations { get; set; } = new List<Reservation>(); // التدريب له مجموعة حجوزات
  
    public List<Mail>? Mail { get; set; } = new List<Mail>(); // التدريب له مجموعة وجبات
   
    public Client? Clint { get; set; } // الزبون الذي حجز هذا التدريب
    public int ClientId { get; set; } 
}
