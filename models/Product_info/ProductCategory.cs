using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Product_info;

public class ProductCategory
{
    [ForeignKey("Product")]
    public string ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("Category")]
    public string CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}