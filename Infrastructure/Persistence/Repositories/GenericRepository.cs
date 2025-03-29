using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>:IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private protected readonly StoreContext _dbContext;
        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithNoTracking = true)
        {
            if (WithNoTracking)
                return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            return await _dbContext.Set<TEntity>().ToListAsync();


        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {

            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)=>await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);
       

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);


        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _dbContext.Set<TEntity>();
        }
    }
}

