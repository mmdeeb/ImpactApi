using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class RestaurantAccount : BaseAuditableEntity
{
    public double TotalBalance { get ; set ; }
    public double Debt { get; set; }

    public Restaurant? Restaurant { get; set; }

    public List<Mail>? Mails { get; set; } = new List<Mail>();
    public List<ReceiptToRestaurant>? ReceiptToRestaurants { get; set; } = new List<ReceiptToRestaurant>();
}
