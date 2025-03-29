using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Shared;

namespace Services
{
    internal class ProductService(IUnitOfWork _unitOfWork,IMapper mapper) : IProductService
    {
       
        public Task<int> CreateProductAsync(ProductResultDTO product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var Brands=await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var BrandResult=mapper.Map<IEnumerable<BrandResultDTO>>(Brands);
            return BrandResult;
        }

        public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync(string? Search=null)
        {
            var Products=await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var ProductsResult = mapper.Map<IEnumerable<ProductResultDTO>>(Products);
            return ProductsResult;
        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
        {
            var Types =await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesResult = mapper.Map<IEnumerable<TypeResultDTO>>(Types);
            return TypesResult;
        }

        public async Task<ProductResultDTO?> GetProductByIdAsync(int id)
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var ProductResult = mapper.Map<ProductResultDTO>(Product);
            return ProductResult;
        }

        public Task<int> UpdateProductAsync(ProductResultDTO product)
        {
            throw new NotImplementedException();
        }
       
    }
}
