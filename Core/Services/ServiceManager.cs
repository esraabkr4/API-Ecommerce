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
        private readonly Lazy<IBasketService> _BasketService;


        public ServiceManager(IUnitOfWork _unitOfWork, IMapper mapper,IBasketRepository basketRepository)
        {
            _productService = new  Lazy<IProductService>(valueFactory:()=>new ProductService(_unitOfWork,mapper));
            _BasketService = new Lazy<IBasketService>(valueFactory: () => new BasketService(basketRepository, mapper));

        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _BasketService.Value;
    }
}
