using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Client : User
{
    public List<Training>? Trainings { get; set; } = new List<Training>();
    public int? ClientAccountId { get; set; }

    public ClientAccount? ClientAccount { get; set; }
}