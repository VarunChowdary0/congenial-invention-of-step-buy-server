using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Logistics;

public class Item
{
    [ForeignKey("User")]
    public string CartOf { get; set; } = string.Empty; // One user has many Cartitems in Cart.
   
    [ForeignKey("product")]
    public string ProductId { get; set; } 
    
    public int Quantity { get; set; } = 0;
}