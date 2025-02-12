using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using step_buy_server.DTO;

namespace step_buy_server.models.Logistics;

public class OrderItem:Item
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime DateOrdered { get; set; } = DateTime.UtcNow;

    
    [ForeignKey("Bill")]
    public string? BillId { get; set; } = string.Empty;
    public Bill? Bill { get; set; }

    [ForeignKey("Delivery")]
    public string? DeliveryId {get;set;} = string.Empty;  // one Delivery has one item Ordered.
    public Delivery? Delivery { get; set; }
}