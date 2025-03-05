using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Personal;
using step_buy_server.models.Product_info;

namespace step_buy_server.controller.product;

[Route("api/[controller]")]
[ApiController]
public class ProductController: ControllerBase
{
    private AppDBConfig _context;

    public ProductController(AppDBConfig context)
    {
        this._context = context;
    }
    
    // ✅ 1. GET Product by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        // Console.ForegroundColor = ConsoleColor.Magenta;
        // Console.WriteLine(id);
        // Console.ResetColor();
        var product = await _context.Products
            .Include(p => p.Media)
            .Include(p => p.Reviews)!
                    .ThenInclude(R => R.Media)
            .Include(p=>p.Features)
            .Include(p=>p.ProductCategories)!
                    .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (product == null)
            return NotFound(new { message = $"Product with {id} is not found on the database.   " });

        if (product.ProductCategories != null)
        {
            var mappedProduct = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Rating = product.Rating,
                ImageLink = product.ImageLink,
                ActualPrice = product.ActualPrice,
                Discount = product.Discount,
                Description = product.Description,
                Stock = product.Stock,
                LowStockAlertThreshold = product.LowStockAlertThreshold,
                IsAvailable =product.IsAvailable,
                Media = product.Media,
                Features = product.Features,
                Reviews = product.Reviews,
                ProductCategories = product.ProductCategories,
                DateCreated = product.DateCreated,
                Categories = product.ProductCategories.Select(pc => new Category()
                {
                    Id = pc.CategoryId,
                    Name = pc.Category.Name
                }),
            };
            
            return mappedProduct;
        }

        return product;
    }
    
    // ✅ 2. GET Products by Category
    [HttpGet("bycategory/{category}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
    {
        var products = await _context.Products
            .Include(p => p.Media)
            .Include(p => p.Reviews)!
            .ThenInclude(R => R.Media)
            .Include(p => p.Features)
            .Include(p => p.ProductCategories)!
            .ThenInclude(pc => pc.Category)
            .Where(p => p.ProductCategories != null &&
                        p.ProductCategories.Any(pc => pc.Category.Name == category))
            .ToListAsync();
        return Ok(products);
    }
    
    //✅ 3. get All
    [HttpGet("all/")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Media)
            .Include(p => p.Reviews)!
            .ThenInclude(r => r.Media)
            .Include(p => p.Features)
            .Include(p => p.ProductCategories)!
            .ThenInclude(pc => pc.Category)
            .ToListAsync();

        // Mapping Category Names to the Categories field
        var mappedProducts = products.Select(p => new Product
        {
            Id = p.Id,
            Name = p.Name,
            Rating = p.Rating,
            ImageLink = p.ImageLink,
            ActualPrice = p.ActualPrice,
            Discount = p.Discount,
            Description = p.Description,
            Stock = p.Stock,
            LowStockAlertThreshold = p.LowStockAlertThreshold,
            IsAvailable =p.IsAvailable,
            Media = p.Media,
            Features = p.Features,
            Reviews = p.Reviews,
            ProductCategories = p.ProductCategories,
            DateCreated = p.DateCreated,
            Categories = p.ProductCategories?.Select(pc => new Category
            {
                Id = pc.Category.Id,
                Name = pc.Category.Name
            }).ToList() // Assign category list explicitly
        }).ToList();

        return Ok(mappedProducts);
    }

    
    //4. GET: api/product/search/{name}
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByFilters(
        [FromQuery] string? name, 
        [FromQuery] string? id, 
        [FromQuery] string? category)
    {
        IQueryable<Product> query = _context.Products
            .Include(p => p.ProductCategories)!
            .ThenInclude(pc => pc.Category);

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }
        if (!string.IsNullOrEmpty(id))
        {
            query = query.Where(p => p.Id.Contains(id));  // No need for `.Value`
        }

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.ProductCategories != null && p.ProductCategories.Any(pc => pc.Category.Name.Contains(category)));
        }

        var products = await query.ToListAsync();
        return Ok(products);
    }
    
    //5.  POST: api/product
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    
    //6. PUT: api/product/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, Product updatedProduct)
    {
        var existingProduct = await _context.Products.FindAsync(id)
            ;
        if (existingProduct == null)
            return NotFound();

        // Update only provided fields
        if (!string.IsNullOrWhiteSpace(updatedProduct.Name))
            existingProduct.Name = updatedProduct.Name;

        if (!string.IsNullOrWhiteSpace(updatedProduct.ImageLink))
            existingProduct.ImageLink = updatedProduct.ImageLink;

        if (updatedProduct.ActualPrice != 0) // Assuming 0 means no change
            existingProduct.ActualPrice = updatedProduct.ActualPrice;

        if (!string.IsNullOrWhiteSpace(updatedProduct.Description))
            existingProduct.Description = updatedProduct.Description;

        if (updatedProduct.Rating != 0) // Assuming 0 means no change
            existingProduct.Rating = updatedProduct.Rating;
        if(updatedProduct.Discount != null)
            existingProduct.Discount = updatedProduct.Discount;
        if(updatedProduct.Stock != null)
            existingProduct.Stock = updatedProduct.Stock;
        if(updatedProduct.LowStockAlertThreshold != null)   
            existingProduct.LowStockAlertThreshold = updatedProduct.LowStockAlertThreshold;
        existingProduct.IsAvailable = updatedProduct.IsAvailable;

        await _context.SaveChangesAsync();
        // return Ok(GetProductById(id));
        var product = await _context.Products
            .Include(p => p.Media)
            .Include(p => p.Reviews)!
            .ThenInclude(R => R.Media)
            .Include(p=>p.Features)
            .Include(p=>p.ProductCategories)!
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return NotFound(new { message = $"Product with {id} is not found on the database.   " });
        
        var mappedProduct = new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Rating = product.Rating,
            ImageLink = product.ImageLink,
            ActualPrice = product.ActualPrice,
            Discount = product.Discount,
            Description = product.Description,
            Stock = product.Stock,
            IsAvailable = product.IsAvailable,
            LowStockAlertThreshold = product.LowStockAlertThreshold,
            Media = product.Media,
            Features = product.Features,
            Reviews = product.Reviews,
            ProductCategories = product.ProductCategories,
            DateCreated = product.DateCreated,
            Categories = product.ProductCategories.Select(pc => new Category()
            {
                Id = pc.CategoryId,
                Name = pc.Category.Name
            }),
        };
        return Ok(mappedProduct);
    }
    
    
    //P.id,P.name,ActualPrice,discount,description,
    //imageLink,isAvailable,C.name as Category ,concat(F.Attribute,concat(" ",F.Value)) as Feature
    [HttpGet("deepsearch")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByFilters([FromQuery] string? query)
    {
        
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty.");
        }

        

        var products = await _context.Products
            .Include(p => p.ProductCategories)!
            .ThenInclude(pc => pc.Category)
            .Include(p => p.Features)
            .Where(p => p.IsAvailable &&
                        p.ProductCategories != null &&
                        p.Features != null &&
                        (p.Name.Contains(query) ||
                         p.Description.Contains(query) ||
                         p.ProductCategories.Any(pc => pc.Category.Name.Contains(query)) ||
                         p.Features.Any(f => f.Value.Contains(query))))
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("save_search")]
    public async Task<ActionResult<string>> SaveSearchAsync([FromQuery] string? userId, [FromQuery] string? searchTerm,[FromQuery] string? viewedItem)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(viewedItem))
        {
            // Console.WriteLine("Not Saved"+userId + " , "+searchTerm +" , "+ viewedItem);
            return NoContent();
        }

        var existingSearch = await _context.SearchHistories
            .FirstOrDefaultAsync(s => s.UserId == userId && s.SearchTerm == searchTerm && s.ViewedItem == viewedItem);
        Console.WriteLine(existingSearch.SearchTerm);
        if (existingSearch != null)
        {
            await _context.SearchHistories
                .Where(s => s.Id == existingSearch.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(s => s.SearchCount, s => s.SearchCount + 1)
                    .SetProperty(s => s.LastSearched, s => DateTime.UtcNow));
        }
        else
        {
            _context.SearchHistories.Add(new SearchHistory
            {
                UserId = userId,
                SearchTerm = searchTerm,
                ViewedItem = viewedItem,
                LastSearched = DateTime.UtcNow,
                SearchCount = 1
            });

            await _context.SaveChangesAsync();
        }

        // Console.WriteLine("Saved");
        return Ok(userId);
    }


}

// public class SqlParameter
// {
//     public SqlParameter(string query, string s)
//     {
//         throw new NotImplementedException();
//     }
// }