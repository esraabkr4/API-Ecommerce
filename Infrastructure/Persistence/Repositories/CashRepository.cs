using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class CashRepository(IConnectionMultiplexer connectionMultiplexer) : ICashRepository
    {
        private readonly IDatabase _database=connectionMultiplexer.GetDatabase();
        public async Task<string?> GetAsync(string Key)
        {
            var value=await _database.StringGetAsync(Key);
            return value.IsNullOrEmpty?value:default;
        }

        public async Task SetAsync(string Key, object Value, TimeSpan duration)
        {
            var SerializeObj=JsonSerializer.Serialize(Value);
            await _database.StringSetAsync(Key, SerializeObj, duration);
        }
    }
}
