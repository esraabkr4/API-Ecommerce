using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationParameters
    {
        public ProductSortingOptions? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

    }
    public enum ProductSortingOptions
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc
    }
}
