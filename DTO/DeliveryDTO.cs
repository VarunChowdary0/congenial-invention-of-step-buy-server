using step_buy_server.models.Logistics;
using step_buy_server.models.Personal;

namespace step_buy_server.DTO;

public class DeliveryDTO
{
    public string Id { get; set; } = string.Empty;
    public AddressDTO? Address { get; set; }
    public OrderStatus Status { get; set; }
    public string CurrentLocation { get; set; } = string.Empty;
    public List<string> 
        Instructions { get; set; } = new();
}


// var deliveryDTO = _context.Deliveries
//     .Where(d => d.Id == "some_delivery_id")
//     .Select(d => new DeliveryDTO
//     {
//         Id = d.Id,
//         Address = d.Address != null ? new AddressDTO 
//         {
//             Id = d.Address.Id,
//             Street = d.Address.Street,
//             City = d.Address.City
//         } : null,  // Convert Address to AddressDTO
//
//         Status = d.Status,
//         CurrentLocation = d.Location,
//
//         // Fetch only delivery descriptions to avoid full entity tracking
//         Instructions = d.Instructions.Select(di => di.DeliveryDescription).ToList()
//     })
//     .FirstOrDefault();
