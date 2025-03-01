using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Product_info;

namespace step_buy_server.controller.product;

[Route("api/[controller]")]
[ApiController]
public class ReviewController:ControllerBase
{
    private AppDBConfig _context;

    public ReviewController(AppDBConfig context)
    {
        _context = context;
    }
    // 1. save review;
    // 2. then add image with linking the reviewId;
    
    // get reviews by product

    [HttpGet("reviews/{productid}")]
    public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByProduct(string productid)
    {
        if (!_context.Products.Any(p => p.Id == productid))
        {
            return NotFound();
        }
        var features = await _context.Reviews.Where(f => f.ProductId == productid).ToListAsync();
        return  Ok(features);
    }
    
    // get review

    [HttpGet("{reviewid}")]
    public async Task<ActionResult<Review>> GetReview(string reviewid)
    {
        var review = await _context.Reviews.FindAsync(reviewid);
        if (review == null)
        {
            return NotFound(new { message = "Review not found" });
        }
        
        return review;
    }
    
    //   add review
    [HttpPost("{ProductId}")]
    public async Task<IActionResult> AddReview(string ProductId, ReviewDTO? review)
    {
        var product = await _context.Products.
            Include(p => p.Reviews).
            FirstOrDefaultAsync(p => p.Id == ProductId);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        var user = await _context.Users.FirstOrDefaultAsync(u => review != null && u.Id == review.ReviewerId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }
        var oldReview = await _context.Reviews
                                    .Where(R=>R.ProductId == ProductId
                                              &&
                                              review != null
                                              &&
                                              R.ReviewerId == review.ReviewerId).
                                    FirstOrDefaultAsync();
        if (oldReview != null)
        {
            return BadRequest(new { message = "Review already exists, Please edit it." });
        }

        if (review != null)
        {
            var newReview = new Review()
            {
                ProductId = product.Id,
                ReviewerId = review.ReviewerId,
                Description = review.Description,
                Rating = review.Rating,
            };
            product.Reviews?.Add(newReview);
        }

        await _context.SaveChangesAsync();

        return Ok(review);
    }
    
    // update review

    [HttpPut("{ReviewId}")]
    public async Task<IActionResult> UpdateReview(string ReviewId, ReviewDTO review)
    {
        var reviewToUpdate = await _context.Reviews.FindAsync(ReviewId);
        if (reviewToUpdate == null)
        {
            return NotFound(new { message = "Review not found" });
        }
        reviewToUpdate.Description = review.Description;
        reviewToUpdate.Rating = review.Rating;
        await _context.SaveChangesAsync();
        return Ok(reviewToUpdate);
    }
    
    // delete review

    [HttpDelete("{ReviewId}")]
    public async Task<IActionResult> DeleteReview(string ReviewId)
    {
        var reviewToDelete = await _context.Reviews.FindAsync(ReviewId);
        if (reviewToDelete == null)
        {
            return NotFound(new { message = "Review not found" });
        }
        _context.Reviews.Remove(reviewToDelete);
        await _context.SaveChangesAsync();
        return Ok(reviewToDelete);
    }
    
    
    
}