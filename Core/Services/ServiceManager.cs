using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Shared.Security;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _BasketService;
        private readonly Lazy<IAuthenticationService> _AuthenticationService;


        public ServiceManager(IUnitOfWork _unitOfWork, IMapper mapper,IBasketRepository basketRepository,UserManager<User> userManager,IOptions<JwtOptions> options,IConfiguration configuration)
        {
            _productService = new  Lazy<IProductService>(valueFactory:()=>new ProductService(_unitOfWork,mapper));
            _BasketService = new Lazy<IBasketService>(valueFactory: () => new BasketService(basketRepository, mapper));
            _AuthenticationService = new Lazy<IAuthenticationService>(valueFactory: () => new AuthenticationService(userManager,configuration,options,mapper));

        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _BasketService.Value;

        public IAuthenticationService AuthenticationService => _AuthenticationService.Value;

    }
}
