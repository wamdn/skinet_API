using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{
    private readonly IConfiguration _config;
    
    public DbSet<Product> Products{ get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    public StoreContext(DbContextOptions<StoreContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(_config.GetConnectionString("DefaultConnection"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}
