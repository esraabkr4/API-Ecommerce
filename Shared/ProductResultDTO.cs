using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ProductResultDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        #region ProductBrand
        public string BrandName { get; set; } = null!;
        #endregion
        #region ProductType
        public string TypeName { get; set; } = null!;
        #endregion


    }
}
