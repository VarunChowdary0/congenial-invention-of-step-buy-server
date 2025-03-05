using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Logistics;

namespace step_buy_server.controller.logistics;

[Route("api")]
[ApiController]
public class CartController:ControllerBase
{
    private readonly AppDBConfig _context;

    public CartController(AppDBConfig context)
    {
        _context = context;
    }

    [HttpGet("cart/{userId}")]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetCart(string userId)
    {
        return await _context.CartItems
            .Where(x => x.UserId == userId)
            .Include(x => x.Product)
            .ToListAsync();
    }

    [HttpPost("cart")]
    public async Task<ActionResult<CartItem>> GetCartItems(CartItemDTO cartDTO)
    {
        var check = await _context.CartItems
            .FirstOrDefaultAsync(x => x.UserId == cartDTO.UserId && x.ProductId == cartDTO.ProductId);
        Console.WriteLine(cartDTO.UserId);
        Console.WriteLine(cartDTO.ProductId);
        if (check != null || cartDTO.UserId == null || cartDTO.ProductId == null)
        {
            return BadRequest();
        }

        var newCartItem = new CartItem()
        {
            UserId = cartDTO.UserId,
            ProductId = cartDTO.ProductId,
            Quantity = cartDTO.Quantity,
            Status = cartDTO.Status,
        };
        Console.WriteLine("Assigned id: "+ newCartItem.Id);
        // newCartItem.Id = newCartItem.Id;
        try
        {
            await _context.CartItems.AddAsync(newCartItem);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest();
        }
        return Ok(newCartItem);
    }

    [HttpPut("cart")]
    public async Task<ActionResult<CartItem>> UpdateCartItems(CartItemDTO cartItemDTO)
    {
        Console.WriteLine("item id : "+cartItemDTO.id);
        Console.WriteLine(cartItemDTO.UserId);
        Console.WriteLine(cartItemDTO.ProductId);
        Console.WriteLine(cartItemDTO.Quantity);
        Console.WriteLine(cartItemDTO.Status);
        if (cartItemDTO.id == null)
        {
            return BadRequest("Cart item ID is required.");
        }
        
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemDTO.id);
        
        if (cartItem == null)
        {
            return NotFound("Cart item not found.");
        }
        
        if (cartItemDTO.Quantity <= 0)
        {
            Console.WriteLine("Deleting cart item");
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Cart item deleted." });
        }

        Console.WriteLine($"Before Update - Quantity: {cartItem.Quantity}, Status: {cartItem.Status}");

        // Update fields
        cartItem.Quantity = cartItemDTO.Quantity;
        cartItem.Status = cartItemDTO.Status;

        try
        {
            await _context.SaveChangesAsync();
            Console.WriteLine($"After Update - Quantity: {cartItem.Quantity}, Status: {cartItem.Status}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating cart item: {e.Message}");
            return BadRequest("Failed to update cart item.");
        }

        return Ok(cartItem);
    }

}
