using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
    public class CashService(ICashRepository cashRepository) : ICashService
    {
        public async Task<string> GetCashedValueAsync(string CashKey)
        {
            return await cashRepository.GetAsync(CashKey);
        }

        public async Task SetCashedValueAsync(string CashKey, object Value, TimeSpan duration)
        {
            await cashRepository.SetAsync(CashKey, Value, duration);
        }
    }
}
