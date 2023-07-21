using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Models.Entities;
using Catalog.Infra.Data.Mappings;
using FluentValidation.Results;
using Ifood.Core.Data;
using Ifood.Core.Mediator;
using Ifood.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra
{
    public class ProductContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ProductContext(DbContextOptions<ProductContext> options, IMediatorHandler mediatorHandler)
           : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }


        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

             modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductContext).Assembly);
            // modelBuilder.ApplyConfiguration(new ProductMapping());


            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);


            base.OnModelCreating(modelBuilder);
        }
        
        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DtaCreated") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DtaCreated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DtaCreated").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublishEventDbContext(this);

            return sucesso;
        }
    }

}