using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Application.Commands.AddProduct;
using Catalog.Application.Events;
using Catalog.Application.Queries;
using Ifood.Core.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Configurations
{
    public static class DependencyInjectionConfigApplication
    {
          public static void RegisterServices(this IServiceCollection services){

            services.AddScoped<IRequestHandler<AddProductCommand, CommandHandlerOutput<AddProductCommandOutput>>, ProductHandler>();
            services.AddScoped<IProductQueries, ProductQueries>();

            services.AddScoped<INotificationHandler<ProductRegisteredEvent>, ProductEventHandler>();

        }
    }
}