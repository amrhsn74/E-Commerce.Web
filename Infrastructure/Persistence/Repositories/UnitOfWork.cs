using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            //if (_repositories.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity,TKey>) _repositories[typeName];

            // Refactoring:

            if (_repositories.TryGetValue(typeName,out object? value))
                return (IGenericRepository<TEntity, TKey>)value;

            else
            {
                // Create repo
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                // Store Obj in Dictionary
                _repositories["typeName"] = Repo;
                // Return Obj
                return Repo;
            }
        }
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
