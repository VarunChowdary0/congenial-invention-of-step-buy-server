using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace step_buy_server.models.Product_info
{
    public class Product
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, 5)]
        public double Rating { get; set; } = 0.0;

        public string ImageLink { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public double ActualPrice { get; set; } = 0.0;

        [NotMapped]
        public double Price => ActualPrice - (ActualPrice * Discount / 100);

        [Range(0, 100)]
        public double Discount { get; set; } = 0.0;
        
        [Column(TypeName = "TEXT")]
        public string Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Stock { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int LowStockAlertThreshold { get; set; } = 3;

        public bool IsAvailable { get; set; } = false;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // Navigation properties
        // virtual to prevent unnecessary querying 
        public virtual ICollection<Media>? Media { get; set; } = new HashSet<Media>(); // one product has many media
        public virtual ICollection<Feature>? Features { get; set; } = new HashSet<Feature>(); // one product has many features
        public virtual ICollection<Review>? Reviews { get; set; } = new HashSet<Review>(); // one product has many reviews
        [JsonIgnore]
        public IEnumerable<ProductCategory>? ProductCategories { get; set; }
        public virtual IEnumerable<Category>? Categories { get; set; }
    }
}