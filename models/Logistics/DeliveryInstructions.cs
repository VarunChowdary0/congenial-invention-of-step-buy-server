using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Logistics;

public class DeliveryInstructions
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string Instruction { get; set; } = string.Empty;

    [ForeignKey("Delivery")]
    public string DeliveryId { get; set; } = string.Empty;
    public Delivery? Delivery { get; set; } // Navigation property
}
