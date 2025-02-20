using Azure.Identity;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Infrastructure.Data;
using DukandaCore.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped<IUser, CurrentUser>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        // Customise default API behaviour
        builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
            { 
                Name = "Authorization", 
                Type = SecuritySchemeType.ApiKey, 
                Scheme = "Bearer", 
                BearerFormat = "JWT", 
                In = ParameterLocation.Header, 
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\nExample: \"Bearer 12345abcdef\"", 
            }); 
            options.AddSecurityRequirement(new OpenApiSecurityRequirement 
            { 
                { 
                    new OpenApiSecurityScheme 
                    { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = "Bearer" 
                        } 
                    }, 
                    new string[] {} 
                } 
            });  
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Dukanda API", Version = "v1", Description = "API documentation for Dukanda Core"
                });
        });
        //tmp
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }
}
