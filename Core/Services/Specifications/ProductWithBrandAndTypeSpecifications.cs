using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications:Specifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(int id)
            :base(product=>product.Id == id)
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.productType);
        }
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters)
            :base(product=>(!parameters.BrandId.HasValue || product.BrandId==parameters.BrandId.Value) &&
        (!parameters.TypeId.HasValue || product.TypeId==parameters.TypeId.Value))
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.productType);
            #region Sort
            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case (ProductSortingOptions.NameAsc):
                        SetOrderBy(P => P.Name);
                        break;
                        case (ProductSortingOptions.NameDesc):
                        SetOrderByDescending(P => P.Name);
                        break;
                    case (ProductSortingOptions.PriceAsc):
                        SetOrderBy(P => P.Price);
                        break;
                    case (ProductSortingOptions.PriceDesc):
                        SetOrderByDescending(P => P.Price);
                        break;
                }
            }
            #endregion
        }
    }
}
