﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using step_buy_server.models.Personal;

namespace step_buy_server.models.Logistics;

public class Delivery
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [ForeignKey("Address")]
    public string? AddressId { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string CurrentLocation { get; set; } = string.Empty;
    
    public virtual ICollection<DeliveryInstructions> DeliveryInstructions { get; set; } = new HashSet<DeliveryInstructions>();
}

public enum OrderStatus
{
    Pending ,
    Delivered ,
    Shipped ,
    Returned ,
    Cancelled ,
}