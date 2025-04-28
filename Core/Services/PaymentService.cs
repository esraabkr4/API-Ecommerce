using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Shared;
using Stripe;
using Product = Domain.Entities.Product;

namespace Services
{
    public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("StripeSettings")["Secretkey"];
            var basket = await basketRepository.GetBasketAsync(BasketId)
                ?? throw new EntityNotFoundException<string>(BasketId, "Basket");
            foreach (var item in basket.Items)
            {
                var Product = await unitOfWork.GetRepository<Product, int>()
                    .GetByIdAsync(item.Id) ?? throw new EntityNotFoundException<int>(item.Id, "Product");

                item.Price = Product.Price;
            }
            if (!basket.deliveryMethodId.HasValue) throw new Exception("No Delivery Method Is Selected");
            var Method = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.deliveryMethodId.Value) ?? throw new EntityNotFoundException<int?>(basket.deliveryMethodId, "Delivery Method");
            basket.ShippingPrice = Method.Price;
            var Amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;
            if (string.IsNullOrWhiteSpace(basket.PaymentIntendId))
            {
                var CreateOptions = new PaymentIntentCreateOptions
                {
                    Amount = Amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = new PaymentIntentService().CreateAsync(CreateOptions);
                basket.PaymentIntendId = paymentIntent.Result.Id;
                basket.ClientSecret = paymentIntent.Result.ClientSecret;
            }
            else
            {
                var UpdatedOptions = new PaymentIntentUpdateOptions
                {
                    Amount = Amount,

                };
                await new PaymentIntentService().UpdateAsync(basket.PaymentIntendId, UpdatedOptions);
            }
            await basketRepository.UpdateBasketAsync(basket);
            return mapper.Map<BasketDto>(basket);
        }

    }
}
