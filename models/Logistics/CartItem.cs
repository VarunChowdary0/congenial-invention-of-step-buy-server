using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using step_buy_server.models.Product_info;

namespace step_buy_server.models.Logistics;

public class CartItem:Item
{
    [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
    public ItemStatus Status { get; set; } = ItemStatus.Default;
    
    public virtual Product? Product { get; set; }
}

public enum ItemStatus
{
    Default  = 1,
    SaveForLater = 2
}

//
//
// {
// "productId": "3dacaa1a-12fc-4d84-bdbd-817f6a019e1c",
// "userId": "52329816-6a2d-4a58-85d2-567dd2e730e3",
// "quantity": 10,
// "status": 1
// }