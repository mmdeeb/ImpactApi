namespace Impact.Api.Models;

public class EmployeeAccountDTO
{
    public int Id { get; set; }
    public double? Deduct { get; set; }
    public double? AdvancePayment { get; set; }
    public double? Reward { get; set; }
    public double TotalBalance { get; set; }
    public double Debt { get; set; }
}
