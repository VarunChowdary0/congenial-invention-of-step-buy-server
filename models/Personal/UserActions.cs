using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Personal;

public class UserActions
{
    [Key]
    public string Id {set;get;} = Guid.NewGuid().ToString();
    
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("Product")]
    public string ProductId { get; set; } = string.Empty;

    // Actions ?? 
    public int ActionCount { get; set; } = 1;
    
    public bool Clicked { get; set; } = false;
    public bool Ignored { get; set; } = false;

    public SeeingAction MediaAction { get; set; } = SeeingAction.Level0;
    public SeeingAction FeatureAction { get; set; } = SeeingAction.Level0;
    public SeeingAction ReviewAction { get; set; } = SeeingAction.Level0;
    public SeeingAction SimilarityAction { get; set; } = SeeingAction.Level0;
    
    public DateTime LastActionTime { get; set; } = DateTime.UtcNow;

}

public enum SeeingAction
{
    Level0 = 0,
    Level1 = 1,
    Level2 = 2,
    Level3 = 3
}