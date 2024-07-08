using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Hall : BaseAuditableEntity
{
    public string? HallName { get; set;}
    public Center? Center { get; set;} // قاعة خاصة بمركز واحد

    public int CenterId { get; set; }
    public List<Reservation>? Reservations { get; set; } = new List<Reservation>(); // القاعة لديها اكثر من حجز
    public string? ListDetials { get; set; }

}
