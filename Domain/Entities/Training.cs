using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Entities;

public class Training : BaseAuditableEntity
{
    public string? TrainingName { get; set; }
    public int NumberOfStudents { get; set; }
    public string? TrainingDetails { get; set;}
 
   
    public List<Attendance>? Attendances { get; set; } = new List<Attendance>(); 
 
    public int TrainingInvoiceId { get; set; } 
    public TrainingInvoice? TrainingInvoice { get; set; }
  
    public List<Trainee>? Trainees { get; set; } = new List<Trainee>(); 
  
    public List<Reservation>? Reservations { get; set; } = new List<Reservation>(); 
     
    public Client? Client { get; set; } 
    public int ClientId { get; set; } 
}
