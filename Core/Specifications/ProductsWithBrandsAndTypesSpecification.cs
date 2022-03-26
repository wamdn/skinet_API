using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
{
    public ProductsWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
    {
        AddProductIncludes();
    }

    public ProductsWithBrandsAndTypesSpecification(string? sortBy = "", bool? isDesc = false)
    {
        AddProductIncludes();
        AddProductOrderBy(sortBy ?? string.Empty);
        IsDescendingOrder = isDesc ?? false;
    }

    private void AddProductOrderBy(string sortBy)
    {
        Expression<Func<Product, object>> orderByExpression =
            sortBy.ToLower() switch
            {
                "name" => product => product.Name,
                "price" => product => product.Price,
                _ => product => product.Name
            };

        AddOrderBy(orderByExpression);
    }

    private void AddProductIncludes()
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }
}