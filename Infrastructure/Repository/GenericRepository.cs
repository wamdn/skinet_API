using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly StoreContext _db;

    public GenericRepository(StoreContext db)
    {
        _db = db;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _db.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IReadOnlyList<TEntity>> ListAllAsync()
    {
        return await _db.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetOneWithSpecAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TEntity>> ListWithSpecAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>().AsQueryable();
        query = SpecificationEvaluator<TEntity>.GetQuery(query, spec);
        return query;
    }
}