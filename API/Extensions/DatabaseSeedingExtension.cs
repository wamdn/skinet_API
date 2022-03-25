using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class DatabaseSeedingExtension
{
    public static async Task<IApplicationBuilder> UseDatabaseSeeding(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        // Database update at startup
        IServiceProvider service = scope.ServiceProvider;
        ILoggerFactory loggerFactory = service.GetRequiredService<ILoggerFactory>();

        try
        {
            StoreContext ctx = service.GetRequiredService<StoreContext>();
            // Create database with tables
            await ctx.Database.MigrateAsync();
            // Seed the tables only if there are empty
            await StoreContextSeed.SeedAsync(ctx, loggerFactory);
        }
        catch (Exception ex)
        {
            ILogger logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex, "An error occurred during migration");
        }
        
        return app;
    }
}