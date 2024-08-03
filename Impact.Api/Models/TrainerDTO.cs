namespace Impact.Api.Models
{
    public class TrainerDTO
    {
        public int Id { get; set; }
        public string? TrainerName { get; set; }
        public string? ImgLink { get; set; }

        public string? ListSkills { get; set; }
        public string? TrainerSpecialization { get; set; }
        public string? Summary { get; set; }
        public string? CV { get; set; }
    }
}
