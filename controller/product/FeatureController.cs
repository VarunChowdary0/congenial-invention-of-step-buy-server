using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Product_info;

namespace step_buy_server.controller.product;


[Route("api/[controller]")]
[ApiController]
public class FeatureController:ControllerBase
{
    private AppDBConfig _context;

    public FeatureController(AppDBConfig context)
    {
        _context = context;
    }

    [HttpGet("{Productid}")]
    public async Task<ActionResult<IEnumerable<Feature>>> GetProductInfo(string Productid)
    {
        if (!_context.Products.Any(p => p.Id == Productid))
        {
            return NotFound();
        }
        var features = _context.Features.Where(f => f.ProductId == Productid);
        return await features!.ToListAsync();
    }
    
    // add feature
    
    [HttpPost("{Productid}")]
    public async Task<IActionResult> AddFeature(string Productid, FeatureDTO feature)
    {
        var product = await _context.Products.
            Include(p => p.Features).
            FirstOrDefaultAsync(p => p.Id == Productid);
        if (product == null)
            return NotFound();

        Feature newFeature = new Feature()
        {
            ProductId = product.Id,
            Attribute = feature.Attribute,
            Value = feature.Value
        };

        product.Features?.Add(newFeature);
        await _context.SaveChangesAsync();
        

        return Ok(newFeature);
    }
    
    // update feature

    [HttpPut("{featureId}")]
    public async Task<IActionResult> UpdateFeature(string featureId, FeatureDTO feature)
    {
        var FoundFeature = await _context.Features.FirstOrDefaultAsync(p => p.Id == featureId);
        if (FoundFeature == null)
        {
            return NotFound();
        }
        FoundFeature.Attribute = feature.Attribute;
        FoundFeature.Value = feature.Value;
        await _context.SaveChangesAsync();
        return Ok(feature);
    }
    
    // delete feature

    [HttpDelete("{featureId}")]
    public async Task<IActionResult> DeleteFeature(string featureId)
    {
        var FoundFeature = await _context.Features.FirstOrDefaultAsync(p => p.Id == featureId);
        if (FoundFeature == null)
        {
            return NotFound(new {message = "Feature not found"});
        }
        _context.Features.Remove(FoundFeature);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}