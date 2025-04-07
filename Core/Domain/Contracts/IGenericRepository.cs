using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithNoTracking = true);
        #region For Specifications
        Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(Specifications<TEntity> specifications);
        Task<TEntity?> GetByIdWithSpecificationsAsync(Specifications<TEntity> specifications);
        #endregion

        Task<TEntity?> GetByIdAsync(int id);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAllAsQueryable();
        Task<int> CountAsync (Specifications<TEntity?> specifications);
    }
}
