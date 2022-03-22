using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MsDemo.Shared
{
    public static class SwaggerExtensions
    {
        public static void ConfigureAuthentication(this SwaggerGenOptions options)
        {
            var scheme = "Bearer";
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter **JWT Bearer** token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = scheme,
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = scheme,
                    Type = ReferenceType.SecurityScheme,
                },
            };
            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, new string[] { } } });
        }
    }
}
