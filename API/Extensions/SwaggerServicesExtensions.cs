﻿using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class SwaggerServicesExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => 
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiNet API", Version = "v1" }));
        
        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => 
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiNet API v1"));
        
        return app;
    }
}