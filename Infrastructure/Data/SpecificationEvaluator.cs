using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecification<TEntity> spec)
    {
        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (spec.Includes.Any())
            query = spec.Includes.Aggregate(query, (acc, cur) => acc.Include(cur));
        
        return query;
    }
}