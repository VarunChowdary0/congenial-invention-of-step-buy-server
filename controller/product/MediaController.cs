using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Product_info;

namespace step_buy_server.controller;


[Route("api/[controller]")]
[ApiController]
public class MediaController:ControllerBase
{
    private AppDBConfig _context;

    public MediaController(AppDBConfig context)
    {
        _context = context;
    }
    
    // get media
    // 0 -> review
    // 1 -> product
    
    [HttpGet("{mediaFor}/{id}")]
    public async Task<ActionResult<List<Media>>> GetMedia(MediaFor mediaFor, string id)
    {
        var entityExists = mediaFor switch
        {
            MediaFor.Product => await _context.Products.AnyAsync(p => p.Id == id),
            MediaFor.Review => await _context.Reviews.AnyAsync(r => r.Id == id),
            _ => false
        };

        if (!entityExists)
        {
            return NotFound(new { message = $"{mediaFor} not found" });
        }

        var media = await _context.Media
            .Where(m => m.ReferanceId == id && m.MediaFor == mediaFor)
            .ToListAsync();

        return Ok(media);
    }

    // MediaType 0 -> Photo , 1 -> Video
    
    // add review media
    [HttpPost("review/{reviewid}")]
    public async Task<IActionResult> AddReviewMedia(string reviewid,MediaDTO media)
    {
        var review = await _context.Reviews.Include(r=>r.Media)
            .FirstOrDefaultAsync(r => r.Id == reviewid);

        if (review == null)
        {
            return NotFound(new { message = "Review not found" });
        }

        Media newMedia = new Media()
        {
            Type = media.Type,
            MediaFor = MediaFor.Review,
            Link = media.Link,
            ReferanceId = review.Id,
        };
        if (review.Media == null)
        {
            review.Media = new List<Media>();
        }
        review.Media?.Add(newMedia);
        await _context.SaveChangesAsync();
        
        return Ok(review);
    }
    
    // add product media
    [HttpPost("product/{ProductId}")]
    public async Task<IActionResult> AddProductMedia(string ProductId,MediaDTO media)
    {
        var product = await _context.Products.Include(r=>r.Media)
            .FirstOrDefaultAsync(p => p.Id == ProductId);

        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }

        Media newMedia = new Media()
        {
            Type = media.Type,
            MediaFor = MediaFor.Product,
            Link = media.Link,
            ReferanceId = product.Id,
        };
        
        if (product.Media == null)
        {
            product.Media = new List<Media>();
        }

        product.Media.Add(newMedia);
        await _context.SaveChangesAsync();
        
        return Ok(product);
    }
}