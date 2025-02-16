using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Product_info;

namespace step_buy_server.controller;

[Route("api/[controller]")]
[ApiController]
public class CategoryController:ControllerBase
{
    private AppDBConfig _context;

    public CategoryController(AppDBConfig context)
    {
        _context = context;
    }
    
    // add category.
    [HttpPost("{Pid}")] 
    public async Task<IActionResult> AddCategory(string Pid, CategoryDTO categoryDTO)
    {
        // Find the product including its categories
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == Pid);
        if (product == null)
        {
            return NotFound();
        }
        
        Console.WriteLine(categoryDTO.Id + "," + categoryDTO.Name);
        var OldCati = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryDTO.Name);
        Category cati = new Category();
        if (OldCati == null)
        {
            cati = new Category()  // Use cati instead of OldCati
            {
                Name = categoryDTO.Name
            };
            _context.Categories.Add(cati);
            await _context.SaveChangesAsync();  // Ensure cati.Id is populated
            Console.WriteLine("New category created");
            OldCati = cati;  // Assign the newly created category
        }
        else
        {
            Console.WriteLine("category already exists");
        }

        var n_PC = new ProductCategory()
        {
            CategoryId = OldCati.Id,  // Ensure this is a valid existing ID
            ProductId = product.Id
        };
        
        _context.ProductCategories.Add(n_PC);
        await _context.SaveChangesAsync();
        var result = new CategoryDTO() { Name = categoryDTO.Name, Id = OldCati.Id };
        return Ok(new { message = "Category added and mapped successfully.", n_PC, result });
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await _context.Categories
            .Select(C => new CategoryDTO
            {
                Id = C.Id,
                Name = C.Name
            })
            .ToListAsync();

        return Ok(categories);
    }


    [HttpDelete("{categoryid}")]
    public async Task<IActionResult> DeleteCategory(string categoryid)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryid);
        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Category deleted." });
    }

    [HttpDelete("Link/{categoryid}/{productid}")]
    public async Task<IActionResult> DeleteProduct(string categoryid, string productid)
    {
        var link = await _context.ProductCategories
            .FirstOrDefaultAsync(c 
                => c.CategoryId == categoryid && c.ProductId == productid);
        if (link == null)
        {
            return NotFound(new { message = "Product not found." });
        }
        _context.ProductCategories.Remove(link);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Product deleted." });
    }

    [HttpGet("{search}")]
    public async Task<ActionResult<IEnumerable<Category>>> Search(string search)
    {
        var categories = await _context.Categories.Where(c => c.Name.Contains(search)).ToListAsync();
        return Ok(categories);
    }
}