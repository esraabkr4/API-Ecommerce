using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions options):base(options) { }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
        }

        #region DBsets
        public DbSet<Product> products { get; set; }
        public DbSet<ProductType> productsTypes { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        #endregion
    }
}
