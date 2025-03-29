using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity:BaseEntity<TKey>;
        Task<int> CompleteAsync();
    }
}
