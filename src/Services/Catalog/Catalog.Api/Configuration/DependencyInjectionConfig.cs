using Catalog.Application.Configurations;
using Catalog.Domain.Models.Repositories;
using Catalog.Infra;
using Catalog.Infra.Configurations;
using Catalog.Infra.Data.Repository;
using FluentValidation;
using Ifood.Core.Mediator;
using MediatR;

namespace Catalog.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
          public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program).Assembly);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            DependencyInjectionConfigInfra.RegisterServices(services);
            DependencyInjectionConfigApplication.RegisterServices(services);
        }
    }
}