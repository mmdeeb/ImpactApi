using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SubTraining : BaseAuditableEntity
{
    public string? SubTrainingName { get; set;} 
    public string? SubTrainingDescription { get; set;}
    public string? ImgLink { get; set; }

    public List<Trainer>? Trainers { get; set; } = new List<Trainer>();
   
    public TrainingType? TrainingType { get; set; }
    public int TrainingTypeId { get; set; }
}
