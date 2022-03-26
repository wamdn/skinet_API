using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecification<TEntity> spec)
    {
        // Add WHERE clause to query
        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        // Add JOIN clauses to query
        if (spec.Includes.Any())
            query = spec.Includes.Aggregate(
                query, 
                (queryable, expression) => queryable.Include(expression)
            );
        
        // Add ORDER BY clause to query
        if (spec.OrderBy is not null)
            query = spec.IsDescendingOrder 
                ? query.OrderByDescending(spec.OrderBy)
                : query.OrderBy(spec.OrderBy);
        
        return query;
    }
}
