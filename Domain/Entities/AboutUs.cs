namespace Domain.Entities;

public class AboutUs : BaseAuditableEntity
{
    public string? ListContactInfo { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? Links { get; set; }

}
