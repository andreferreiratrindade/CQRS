using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Models.Entities;
using Catalog.Domain.Models.Repositories;
using Ifood.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Product product)
        {
           _context.Products.Add(product);
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

    }
}