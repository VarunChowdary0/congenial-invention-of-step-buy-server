using System.ComponentModel.DataAnnotations;

namespace step_buy_server.models.Product_info;

public class Product
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public double Rating { get; set; } = 0.0;
    public double ActualPrice { get; set; } = 0.0;
    public double Price => ActualPrice - (ActualPrice * Discount / 100);
    public double Discount { get; set; } = 0.0;
    [Range(0, 99)]
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; } = 0;
    public bool IsAvailable => Stock > 0;
    
    
    // virtual to prevent unnecessary querying 
    public virtual ICollection<Media>? Media { get; set; } = new HashSet<Media>(); // one product has many media
    public virtual ICollection<Feature>? Features { get; set; } = new HashSet<Feature>(); // one product has many features
    public virtual ICollection<Review>? Reviews { get; set; } = new HashSet<Review>(); // one product has many reviews
    public virtual ICollection<Category>? Categories { get; set; } = new HashSet<Category>(); // many product has many categories
}