using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public bool IsDescendingOrder { get; protected init; }
    public int PageSize { get; private set; }
    public int PageIndex { get; private set; }
    public bool IsPagingEnabled { get; private set; } = true;

    protected BaseSpecification(Expression<Func<T, bool>>? criteria = null)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddPagination(int pageSize, int pageIndex)
    {
        var isPaginationDisabled = pageSize == default && pageIndex == default;
        if (isPaginationDisabled) return;

        PageSize = pageSize;
        PageIndex = pageIndex;
    }
}