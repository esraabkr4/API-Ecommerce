using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
        public decimal Price { get; set; }
        #region ProductBrand
        public int BrandId { get; set; }
        public ProductBrand productBrand { get; set; }
        #endregion
        #region ProductType
        public int TypeId { get; set; }
        public ProductType productType { get; set; }
        #endregion

    }
}
