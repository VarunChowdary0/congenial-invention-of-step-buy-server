using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace step_buy_server.models.Product_info;

public class ProductCategory
{
    [ForeignKey("Product")]
    public string? ProductId { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("Category")]
    public string? CategoryId { get; set; }
    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;
}