using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntities;

namespace Domain.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
