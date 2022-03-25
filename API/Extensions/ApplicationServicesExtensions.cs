using API.Errors;
using Core.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Custom error response when ModelState is invalid
        services.Configure<ApiBehaviorOptions>(option =>
        {
            option.InvalidModelStateResponseFactory = actionContext =>
            {
                IEnumerable<string> errors = actionContext.ModelState
                    .Where(m => m.Value is not null && m.Value.Errors.Any())
                    .SelectMany(m => m.Value!.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                ApiValidationErrorResponse errorResponse = new (400) { Errors = errors };
        
                return new BadRequestObjectResult(errorResponse);
            };
        });
        
        // Add ProductRepository
        services.AddScoped<IProductRepository, ProductRepository>();
        // Add GenericRepository
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}