﻿namespace Impact.Api.Models;

public class ClientDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? ClientAccountId { get; set; }
}
