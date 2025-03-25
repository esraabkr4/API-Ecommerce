using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Product
            builder.Property(product => product.Price)
                .HasColumnType("decimal(18,3)");
            #endregion

            #region ProductBrand
            builder.HasOne(product => product.productBrand)
                .WithMany()
                .HasForeignKey(product => product.BrandId);
            #endregion

            #region ProductType
            builder.HasOne(product => product.productType)
                .WithMany()
                .HasForeignKey(product => product.TypeId);
            #endregion
        }
    }
}
