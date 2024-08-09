using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Client : BaseAuditableEntity
{
    public string UserId { get; set; }

    public List<Training>? Trainings { get; set; } = new List<Training>();
    public int? ClientAccountId { get; set; }

    public ClientAccount? ClientAccount { get; set; }

}