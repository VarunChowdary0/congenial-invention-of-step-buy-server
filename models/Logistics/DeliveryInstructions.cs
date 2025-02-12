using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Logistics;

public class DeliveryInstructions
{
    [ForeignKey("Delivery")]
    public string DeliveryId { get; set; }

    public string DeliveryDescription { get; set; } = String.Empty;
}