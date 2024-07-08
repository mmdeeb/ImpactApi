using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ReceiptFromClient : Receipt
{
    public ClientAccount? ClientAccount { get; set; }
    public int ClientAccountId { get; set; }
 
}
