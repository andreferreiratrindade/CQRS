using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.IntegrationEvents;
using Ifood.MessageBus;
using MediatR;

namespace Catalog.Application.Events
{
public class ProductEventHandler : INotificationHandler<ProductRegisteredEvent>
    {
         private readonly IMessageBus _bus;
         public ProductEventHandler(IMessageBus bus)
         {
            _bus = bus;
         }

        public async Task Handle(ProductRegisteredEvent message, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new ProductRegisteredIntegrationEvent(message.Id));
        }
    }
}