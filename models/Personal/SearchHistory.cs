using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Personal
{
    public class SearchHistory
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        [Required, Column(TypeName = "TEXT")]
        public string SearchTerm { get; set; } = string.Empty;
        
        [ForeignKey("Product")]
        public string ViewedItem { get; set; } = string.Empty;
        
        [Required]
        public int SearchCount { get; set; } = 0;

        [Required]
        public DateTime LastSearched { get; set; } = DateTime.UtcNow;

        // Navigation property for the User table
        public virtual User? User { get; set; }
    }
}