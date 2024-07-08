namespace Domain.Entities;

public class FinancialFund : BaseAuditableEntity
{
    public double DebtOnTheFund { get; set; }
    public double DebtToTheFund { get; set; }
}
