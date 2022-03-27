using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    bool IsDescendingOrder { get; }
    int PageSize { get; }
    int PageIndex { get; }
}