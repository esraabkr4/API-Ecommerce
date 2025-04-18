using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class ShipingAddress
    {
        public ShipingAddress()
        {
            
        }
        public ShipingAddress(String _FirstName, String _LastName, String _Street, String _City, String _Country)
        {
            FirstName = _FirstName;
            LastName = _LastName;
            Street = _Street;
            City = _City;
            Country = _Country;
        }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
    }
}
