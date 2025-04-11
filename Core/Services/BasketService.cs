using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Exceptions;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository,IMapper Mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
        =>await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var Basket=basketRepository.GetBasketAsync(id);
            return Basket is null ? throw new BasketNotFoundException(id) : Mapper.Map<BasketDto>(Basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket, TimeSpan? timeSpan = null)
        {
            var CustomerBasket=Mapper.Map<CustomerBasket>(basket);
            var UpdatedOrCreatedItem=await basketRepository.UpdateBasketAsync(CustomerBasket);
            return UpdatedOrCreatedItem is null ?
                throw new Exception("We Can't Update Basket"):Mapper.Map<BasketDto>(UpdatedOrCreatedItem);

        }
    }
}
