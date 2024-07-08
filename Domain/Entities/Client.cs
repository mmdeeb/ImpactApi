using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Client : User
{
    public List<Training>? Trainings { get; set; } = new List<Training>(); // الزبون لديه عدد من التدريبات

    public int? ClientAccountId { get; set; } // اجعل هذا المفتاح الأجنبي قابلاً للاحتواء على قيمة null

    public ClientAccount? ClientAccount { get; set; } // الزبون لديه حساب مالي وحيد
}