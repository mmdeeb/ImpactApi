﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Employee : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public string? EmployeeType { get; set; }
    public double Salary { get; set; }

    public Center? Center { get; set; }
    public int CenterId { get; set; }
    public int EmployeeAccountId { get; set; }
    public EmployeeAccount? EmployeeAccount { get; set; }
}
