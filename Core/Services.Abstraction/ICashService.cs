using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICashService
    {
        public Task SetCashedValueAsync(string CashKey, Object Value, TimeSpan duration);
        public Task<string> GetCashedValueAsync(string CashKey);
    }
}
