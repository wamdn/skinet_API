using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _db;

    public ProductRepository(StoreContext db)
    {
        _db = db;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _db.Products
                        .Include(p => p.ProductBrand)
                        .Include(p => p.ProductType)
                        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _db.Products
                        .Include(p => p.ProductBrand)
                        .Include(p => p.ProductType)
                        .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _db.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _db.ProductTypes.ToListAsync();
    }
}