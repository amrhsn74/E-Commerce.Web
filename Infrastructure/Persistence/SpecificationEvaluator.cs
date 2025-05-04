using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity,TKey> Specification) where TEntity : BaseEntity<TKey>
        {
            var Query = InputQuery;

            if (Specification.Criteria != null)
            {
                Query = Query.Where(Specification.Criteria);
            }

            if (Specification.OrderBy != null)
            {
                Query = Query.OrderBy(Specification.OrderBy);
            }
            
            if (Specification.OrderByDescending != null)
            {
                Query = Query.OrderByDescending(Specification.OrderByDescending);
            }
            
            if (Specification.IncludeExpressions != null && Specification.IncludeExpressions.Count > 0)
            {
                //foreach (var Exp in Specification.IncludeExpressions)
                //    Query = Query.Include(Exp);

                Query = Specification.IncludeExpressions.Aggregate(Query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }

            if (Specification.IsPaginated)
            {
                Query = Query.Skip(Specification.Skip).Take(Specification.Take);
            }   
            return Query;
        }
    }
}
