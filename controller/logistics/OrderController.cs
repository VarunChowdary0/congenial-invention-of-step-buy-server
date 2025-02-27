using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.models.Logistics;

namespace step_buy_server.controller.logistics;

[Route("api")]
[ApiController]
public class OrderController:ControllerBase
{
    private readonly AppDBConfig _context;

    public OrderController(AppDBConfig context)
    {
        _context = context;
    }
    
    [HttpGet("Order/all")]
    public  async Task<ActionResult<IEnumerable<OrderItem>>> getOrderItem()
    {
        var Orders = _context
                                    .OrderItems
                                    .Include(o => o.Product)
                                    .ToListAsync();
        return await Orders;
    }

    [HttpGet("Order/{userId}")]
    public async Task<ActionResult<IEnumerable<OrderItem>>> getOrderItem(string userId)
    {
        var orders = await _context
                                            .OrderItems
                                            .Where(o => o.UserId == userId)
                                            .ToListAsync();
        return orders;
    }
}