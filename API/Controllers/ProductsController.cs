using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _db;

    public ProductsController(StoreContext db)
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
    {
        Product? productOrNull = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (productOrNull is null) return BadRequest();

        return Ok(productOrNull);
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        List<Product> products = await _db.Products.AsNoTracking().ToListAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> SaveProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest();

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return Ok(product);
    }

    [HttpPatch]
    public async Task<ActionResult<Product>> patchProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest();

        Product? productToPatchOrNull = await _db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (productToPatchOrNull is null) return BadRequest();

        if (!productToPatchOrNull.Name.Equals(product.Name))
            productToPatchOrNull.Name = product.Name;

        await _db.SaveChangesAsync();
        return Ok(productToPatchOrNull);
    }

    [HttpPut]
    public async Task<ActionResult<Product>> updateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest();

        _db.Products.Update(product);
        await _db.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteProduct([FromRoute] int id)
    {
        Product? product = _db.Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return BadRequest();

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
