using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ifood.Core.DomainObjects;

namespace Catalog.Domain.Models.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string name, int quantity, decimal price)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
        }
        protected Product(){

        }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}