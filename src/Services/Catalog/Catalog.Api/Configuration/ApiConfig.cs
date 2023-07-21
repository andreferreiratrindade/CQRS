using Microsoft.EntityFrameworkCore;
using Catalog.Infra;
using Swashbuckle.AspNetCore.Swagger;


namespace Catalog.Api.Configuration
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<ProductContext>();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("all",
                   builder =>
                       builder
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
            builder.Services.AddMessageBusConfiguration(builder.Configuration);
           
            builder.Services.RegisterServices();

            return builder;

        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseAuthorization();

             app.MapControllers();
            return app;
        }
    }
}