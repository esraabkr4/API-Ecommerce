using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record BasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }

    }
}
