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

        [Required]
        public string ImageLink { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public double ActualPrice { get; set; } = 0.0;

        [NotMapped]
        public double Price => ActualPrice - (ActualPrice * Discount / 100);

        [Range(0, 100)]
        public double Discount { get; set; } = 0.0;

        [Required]
        [Column(TypeName = "TEXT")]
        public string Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Stock { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int LowStockAlertThreshold { get; set; } = 3;

        public bool IsAvailable { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Media> Media { get; set; } = new HashSet<Media>();
        public virtual ICollection<Feature> Features { get; set; } = new HashSet<Feature>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        [JsonIgnore]
        public virtual IEnumerable<ProductCategory>? ProductCategories { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Category>? Categories { get; set; }
    }
}