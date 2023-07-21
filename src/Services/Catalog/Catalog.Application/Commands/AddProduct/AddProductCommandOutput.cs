using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifood.Core.Messages;

namespace Catalog.Application.Commands.AddProduct
{
    public class AddProductCommandOutput
    {
        public Guid Id {get;set;}
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}