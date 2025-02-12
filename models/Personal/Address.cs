using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Personal;

public class Address
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;
    [Required] public string HouseNo { get; set; } = string.Empty;
    public string BuildingName { get; set; } = string.Empty;
    public string PlotNo { get; set; } = string.Empty;
    public string RoadNumber { get; set; } = string.Empty;
    public string ColonyName { get; set; } = string.Empty;
    public string AreaName { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
    public string DistrictName { get; set; } = string.Empty;
    [Required] public string State { get; set; } = string.Empty;
    [Required] public string Country { get; set; } = string.Empty;
    [Required, StringLength(6, MinimumLength = 6 )]
    public string Pin { get; set; } = string.Empty;
}