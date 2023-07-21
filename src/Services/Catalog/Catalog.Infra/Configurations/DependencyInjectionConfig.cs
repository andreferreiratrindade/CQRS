using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Models.Repositories;
using Catalog.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infra.Configurations
{
    public static class DependencyInjectionConfigInfra
    {
        public static void RegisterServices(this IServiceCollection services){
            services.AddScoped<IProductRepository, ProductRepository>();
             services.AddScoped<ProductContext>();
        }
    }
}