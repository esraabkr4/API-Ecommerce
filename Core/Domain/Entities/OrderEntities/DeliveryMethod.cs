using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class DeliveryMethod:BaseEntity<Guid>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string _ShortName, string _Description, decimal _Price)
        {
            ShortName = _ShortName;
            Description = _Description;
            Price = _Price;
        }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
