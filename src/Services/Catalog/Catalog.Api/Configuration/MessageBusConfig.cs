using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ifood.MessageBus;
using Ifood.Core.Utils;
using Catalog.Api.Services;

namespace Catalog.Api.Configuration
{
 public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
             services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<ProductRegisteredIntegrationHandler>();
        }
    }
}