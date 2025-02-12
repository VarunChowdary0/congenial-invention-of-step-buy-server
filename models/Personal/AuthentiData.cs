using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Personal;

public class AuthentiData
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();  

    [Required]
    [ForeignKey("User")]
    public string? UserId { get; set; }

    [Required, StringLength(255, MinimumLength = 8, 
         ErrorMessage = "Password must be between 8 and 255 characters")]
    public string KeyHash { get; set; } = string.Empty;

    // Navigation Property
    public User User { get; set; } = null!;
}