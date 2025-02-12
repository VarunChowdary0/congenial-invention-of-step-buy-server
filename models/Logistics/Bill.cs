using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace step_buy_server.models.Logistics;
public class Bill
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    
    public decimal SubTotal { get; set; } = 0.0m; // Price of items.
    public decimal DeliveryCharge { get; set; } = 0.0m;
    public decimal Tax { get; set; } = 0.0m;  // all tax %
    [NotMapped]
    public decimal TotalAmount => SubTotal + DeliveryCharge + (SubTotal * Tax / 100);
    public BillStatus Status { get; set; }
    public Payment PaymentMethod { get; set; }
}

public enum BillStatus
{
    Completed,
    Pending,
}
    
public enum Payment
{
    UPI,
    CARD,
    EMI,
    NETBANKING,
    COD
}