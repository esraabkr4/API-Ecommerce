using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IUnitOfWork _unitOfWork, IMapper mapper)
        {
            _productService = new  Lazy<IProductService>(valueFactory:()=>new ProductService(_unitOfWork,mapper));
        }

        public IProductService _IProductService => _productService.Value;
    }
}
