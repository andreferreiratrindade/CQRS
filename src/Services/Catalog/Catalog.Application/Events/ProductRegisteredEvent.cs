using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifood.Core.Messages;

namespace Catalog.Application.Events
{
    public class ProductRegisteredEvent : Event
    {
        public ProductRegisteredEvent(Guid id, string name, int quantity, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;

        }
        public Guid Id {get; private set;}
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

    }
}