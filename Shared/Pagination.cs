using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record Pagination<TData>(int? PageIndex,int? PageSize,int? TotalCount,IEnumerable<TData> data)
    {
    }
}
