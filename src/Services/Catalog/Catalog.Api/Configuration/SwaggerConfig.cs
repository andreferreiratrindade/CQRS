using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Catalog.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static  WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {

              builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "CQRS",
                    Description = "CQRS",
                    Contact = new OpenApiContact() { Name = "Andre Trindade", Email = "andreferreiratrindade@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });

            return builder;
        }

        public  static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}