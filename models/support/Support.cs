using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.support;

public class Support:Info
{
    [Required]
    public string Query { get; set; } = string.Empty;
    public string Response  { get; set; } = string.Empty;
    
    public SupportStatus Status { get; set; } = SupportStatus.Pending;
}

public enum SupportStatus
{
    Pending,
    Resolved,
}