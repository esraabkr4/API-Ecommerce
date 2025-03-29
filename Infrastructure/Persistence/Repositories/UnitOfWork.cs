using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            _dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
      => (IGenericRepository<TEntity, TKey>) 
            _repositories.GetOrAdd(key: typeof(TEntity).Name, valueFactory: _ => new GenericRepository<TEntity, TKey>(dbContext: _dbContext));

            //using ordinary repo
            //var typeName = typeof(TEntity).Name;
            //if (_repositories.ContainsKey(key: typeName)) return (IGenericRepository<TEntity, TKey>) _repositories[key: typeName];
            //var repo= new GenericRepository<TEntity, TKey>(dbContext:_dbContext);
            //_repositories.Add(key: typeName,value: repo);
            //return repo;
            //using concurrent repo
       
    }
}
