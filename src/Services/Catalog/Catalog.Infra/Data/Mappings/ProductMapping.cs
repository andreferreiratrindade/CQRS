using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infra.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");
            
            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(5,2)");
            
            builder.Property(c => c.Quantity)
                .IsRequired()
                .HasColumnType("smallint");
                
            builder.ToTable("Products");
        }
    }
}