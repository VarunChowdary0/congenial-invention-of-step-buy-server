using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace step_buy_server.models.Product_info;

public class Review
{
    [Key] 
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Product")] 
    public string ProductId { get; set; } = string.Empty; // the productId which this review is for.
    [ForeignKey("User")]
    public string ReviewerId { get; set; } = string.Empty; // ✅ Explicit FK

    public string Description {get; set;} = string.Empty;
    public double Rating { get; set; } = 0.0;
    
    
    [Required] 
    public DateTime Date { get; set; } = DateTime.UtcNow;
    

    [JsonIgnore]
    public virtual Product? Product { get; set; }
    public virtual ICollection<Media> Media { get; set; } = new HashSet<Media>(); // one review can have many media
    // public int Upvotes { get; set; } = 0; // agreements
    // public int Downvotes { get; set; } = 0; // disagreements
}