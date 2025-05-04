using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);
      
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<TEntity?> GetByIdAsync(TKey id);
        
        void Update(TEntity entity);
        
        void Delete(TEntity entity);

        #region With Specification
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications);

        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,TKey> specifications);

        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);

        #endregion
    }
}
