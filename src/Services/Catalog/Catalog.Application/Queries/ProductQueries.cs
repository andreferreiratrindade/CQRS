using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Catalog.Application.DTO;
using Catalog.Domain.Models.Repositories;

namespace Catalog.Application.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepository;

        public ProductQueries(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<IEnumerable<ProductDTO>> GetAll(Guid clienteId)
        {
            return null;
        }

        public async Task<ProductDTO> GetByProductId(Guid productId)
        {
            const string query = "SELECT * FROM Products WHERE Id = @productId";
            var product = await  _productRepository.GetConnection().QuerySingleAsync<ProductDTO>(query, new {productId});

            return product;
        }
    }
}