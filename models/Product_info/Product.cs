using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace step_buy_server.models.Product_info;

public class Product
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public double Rating { get; set; } = 0.0;
    public string ImageLink { get; set; } = string.Empty;
    public double ActualPrice { get; set; } = 0.0;
    public double Price
    {
        get => ActualPrice - (ActualPrice * Discount / 100);
    }

    public double Discount { get; set; } = 0.0;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; } = 0;
    public int LowStockAlertThreshold { get; set; } = 3;
    
    public bool IsAvailable { get; set; } = true;
    
    public DateTime DateCreated { get; set; } = DateTime.Now;
    
    
    // virtual to prevent unnecessary querying 
    public virtual ICollection<Media>? Media { get; set; } = new HashSet<Media>(); // one product has many media
    public virtual ICollection<Feature>? Features { get; set; } = new HashSet<Feature>(); // one product has many features
    public virtual ICollection<Review>? Reviews { get; set; } = new HashSet<Review>(); // one product has many reviews
    [JsonIgnore]
    public IEnumerable<ProductCategory>? ProductCategories { get; set; }
    public virtual IEnumerable<Category>? Categories { get; set; }
}