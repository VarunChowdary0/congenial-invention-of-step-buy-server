using step_buy_server.models.Logistics;
using step_buy_server.models.Product_info;

namespace step_buy_server.DTO;

public class CartItemDTO:CartItem
{
    public Product Product { get; set; }
}