namespace Domain.Entities;

public class Ads : BaseAuditableEntity
{
    public string? ListAdsMedia { get; set;}
    public string? AdsTitle { get; set;}
    public string? AdsDescription { get; set;}
    public string? AdsLink { get; set;}
}
