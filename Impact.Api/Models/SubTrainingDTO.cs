namespace Impact.Api.Models;
public class SubTrainingDTO
{
    public int Id { get; set; }
    public string? SubTrainingName { get; set; }
    public string? ImgLink { get; set; }

    public string? SubTrainingDescription { get; set; }
    public int TrainingTypeId { get; set; }
}
