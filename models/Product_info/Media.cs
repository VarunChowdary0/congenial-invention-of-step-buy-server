using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace step_buy_server.models.Product_info
{
    public class Media
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string? ReferanceId { get; set; }
        public MediaType Type { get; set; } 
        
        public MediaFor MediaFor { get; set; }

        [Required]
        public string? Link { get; set; } = string.Empty;
    }
    public enum MediaType
    {
        Photo,
        Video
    }

    public enum MediaFor
    {
        Product,
        Review
    }
}