using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext ctx, ILoggerFactory loggerFactory)
    {
        try
        {
            await SeedBrandsAsync(ctx);
            await SeedTypesAsync(ctx);
            await SeedProductsAsync(ctx);
        }
        catch (Exception ex)
        {
            ILogger logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex, "an error occurred will seeding the database");
        }
    }

    private static async Task SeedBrandsAsync(StoreContext ctx)
    {
        if (ctx.ProductBrands.Any()) return;

        string brandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
        IReadOnlyList<ProductBrand>? brands =
            JsonSerializer.Deserialize<IReadOnlyList<ProductBrand>>(brandsData);
        if (brands is null || !brands.Any()) return;

        await ctx.ProductBrands.AddRangeAsync(brands);
        await ctx.SaveChangesAsync();
    }

    private static async Task SeedTypesAsync(StoreContext ctx)
    {
        if (ctx.ProductTypes.Any()) return;

        string typesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");
        IReadOnlyList<ProductType>? types =
            JsonSerializer.Deserialize<IReadOnlyList<ProductType>>(typesData);
        if (types is null || !types.Any()) return;

        await ctx.ProductTypes.AddRangeAsync(types);
        await ctx.SaveChangesAsync();
    }

    private static async Task SeedProductsAsync(StoreContext ctx)
    {
        if (ctx.Products.Any()) return;

        string productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
        IReadOnlyList<Product>? products =
            JsonSerializer.Deserialize<IReadOnlyList<Product>>(productsData);
        if (products is null || !products.Any()) return;

        await ctx.Products.AddRangeAsync(products);
        await ctx.SaveChangesAsync();
    }
}