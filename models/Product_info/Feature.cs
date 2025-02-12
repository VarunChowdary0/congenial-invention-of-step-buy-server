using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Product_info;

public class Feature
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // ✅ Added Key Attribute
    
    [ForeignKey("Product")]
    public string ProductId { get; set; } = String.Empty; // feature ID.
    
    public string Attribute = string.Empty; 
    public string Value = string.Empty;
}