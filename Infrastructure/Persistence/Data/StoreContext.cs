using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions options):base(options) { }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
        }

        #region DBsets
        public DbSet<Product> products { get; set; }
        public DbSet<Domain.Entities.ProductType> productsTypes { get; set; } 
        public DbSet<Domain.Entities.ProductBrand> productBrands { get; set; }
        #endregion
    }
}
