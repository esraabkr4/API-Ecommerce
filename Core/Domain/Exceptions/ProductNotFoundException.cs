using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(int id):base($"Not Found Product With Id:{id}")
        {
            
        }
    }
}
