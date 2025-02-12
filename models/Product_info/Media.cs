using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Product_info
{
    public class Media
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public MediaType Type { get; set; } 

        [Required]
        public string Link { get; set; } = string.Empty;
    }
    public enum MediaType
    {
        Photo,
        Video
    }
}