using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
    {
        Product? productOrNull = await _repo.GetProductAsync(id);
        if (productOrNull is null) return BadRequest();

        return productOrNull;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        IReadOnlyList<Product> products = await _repo.GetProductsAsync();
        return products.ToList();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
        IReadOnlyList<ProductBrand> brands = await _repo.GetProductBrandsAsync();
        return brands.ToList();
    }

    [HttpGet("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        IReadOnlyList<ProductType> types = await _repo.GetProductTypesAsync();
        return types.ToList();
    }
}
