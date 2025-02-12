using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using step_buy_server.models.Personal;
using step_buy_server.models.Product_info;

namespace step_buy_server.models.support;

public class Info
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [ForeignKey("Product")]
    public string ProductId { get; set; } = string.Empty;
    
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    
    public virtual User? User { get; set; } 
    public virtual Product? Product { get; set; } 

}