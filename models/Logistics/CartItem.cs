using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Logistics;

public class CartItem:Item
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public enum ItemStatus
    {
        Default,Deleted,SaveForLater
    }
    public ItemStatus Status { get; set; }
}

//