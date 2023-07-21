using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Models.Entities;
using Ifood.Core.Data;

namespace Catalog.Domain.Models.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Add(Product cliente);

    }
}