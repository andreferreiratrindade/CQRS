using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.IntegrationEvents;
using Ifood.MessageBus;

namespace Catalog.Api.Services
{
    public class ProductRegisteredIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;

        public ProductRegisteredIntegrationHandler(IMessageBus bus)
        {
            _bus = bus;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<ProductRegisteredIntegrationEvent>("ProductRegistered",
                async request => {
                    Console.Write("ID: "+ request.ProductId + " - "+ request.MessageType);
                    await Task.CompletedTask;
                }
            );
        }
    }
}