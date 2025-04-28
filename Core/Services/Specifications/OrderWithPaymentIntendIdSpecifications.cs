using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.OrderEntities;

namespace Services.Specifications
{
    public class OrderWithPaymentIntendIdSpecifications:Specifications<Order>
    {
        public OrderWithPaymentIntendIdSpecifications(String paymentIntentId)
            :base(O=>O.PaymentIntendId== paymentIntentId)
        {
            
        }
    }
}
