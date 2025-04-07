using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstraction
{
    public interface IProductService
    {
        Task<Pagination<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();
        Task<ProductResultDTO?> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(ProductResultDTO product);
        Task<int> UpdateProductAsync(ProductResultDTO product);
        Task<bool> DeleteProductAsync(int id);
    }
}
