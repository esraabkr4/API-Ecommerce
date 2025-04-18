using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EntityNotFoundException<T>:NotFoundException
    {
        public EntityNotFoundException(T id,string entityName):base($"Not Found {entityName} With Id:{id}")
        {
            
        }
    }
}
