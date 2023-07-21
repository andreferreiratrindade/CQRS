using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifood.Core.Messages.Integration;
using Ifood.Message.Bus.ExangesAttributes;

namespace Catalog.Application.IntegrationEvents
{
    [ExchangeName("ProductExchange"), QueueName("ProductRegistered")]
    public class ProductRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid ProductId { get; private set; }

        public ProductRegisteredIntegrationEvent(Guid productId)
        {
            this.ProductId = productId;
   
        }
    }

    public static class TT {
        public static string Teste = "Teste";
    }
}