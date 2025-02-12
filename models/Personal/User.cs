using System.ComponentModel.DataAnnotations;

namespace step_buy_server.models.Personal;

public class User
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Auto-generated ID
    [Required,StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, Phone]
    public string Phone { get; set; } = string.Empty;
    
    // ✅ One User can have multiple Addresses
    public ICollection<Address> Addresses = new HashSet<Address>();
}
