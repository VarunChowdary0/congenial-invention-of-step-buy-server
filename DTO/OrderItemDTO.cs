using step_buy_server.models.Logistics;

namespace step_buy_server.DTO;

public class OrderItemDTO:OrderItem
{
    public Delivery Delivery { get; set; }
}