using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace step_buy_server.models.Product_info;

public class Category
{
    [Key]
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();
}