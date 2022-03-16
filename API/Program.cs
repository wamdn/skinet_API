using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DbContext Sqlite
builder.Services.AddDbContext<StoreContext>();
// Add ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Database update at startup
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider service = scope.ServiceProvider;
    ILoggerFactory loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        StoreContext ctx = service.GetRequiredService<StoreContext>();
        await ctx.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(ctx, loggerFactory);
    }
    catch (Exception ex)
    {
        ILogger logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
    }
}

app.Run();
