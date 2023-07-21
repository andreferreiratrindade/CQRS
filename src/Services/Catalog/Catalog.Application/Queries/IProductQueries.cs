using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.DTO;

namespace Catalog.Application.Queries
{
    public interface IProductQueries
    {
         Task<ProductDTO> GetByProductId(Guid productId);
        Task<IEnumerable<ProductDTO>> GetAll(Guid clienteId);
    }
}