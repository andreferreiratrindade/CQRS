using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.ViewModel
{
    public class AddProductVM
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}