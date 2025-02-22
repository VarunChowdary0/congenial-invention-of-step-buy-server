using step_buy_server.models.Logistics;

namespace step_buy_server.DTO;

public class CartItemDTO
{ 
    public string id { get; set; } = string.Empty;
    public string ProductId { get; set; }
    public string UserId { get; set; }
    public int Quantity { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.Default;
}