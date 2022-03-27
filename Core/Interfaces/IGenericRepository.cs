using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<TEntity>
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IReadOnlyList<TEntity>> ListAllAsync();
    Task<TEntity?> GetOneWithSpecAsync(ISpecification<TEntity> spec);
    Task<IReadOnlyList<TEntity>> ListWithSpecAsync(ISpecification<TEntity> spec);
    Task<int> CountAsync(ISpecification<TEntity> spec);
}