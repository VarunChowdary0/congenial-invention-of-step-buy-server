using System.ComponentModel.DataAnnotations;

namespace step_buy_server.models.support;

public class Reports:Info
{
    [Required]
    public string Description { get; set; } = string.Empty;
}