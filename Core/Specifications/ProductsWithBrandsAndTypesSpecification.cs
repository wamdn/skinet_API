using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
{
    public ProductsWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
    {
        AddProductIncludes();
    }

    public ProductsWithBrandsAndTypesSpecification(ProductSpecParams specParams)
    {
        // join
        AddProductIncludes();

        // sorting
        AddProductOrderBy(specParams.Sort);
        IsDescendingOrder = specParams.Desc;

        // filter
        AddCriteria(p =>
            (p.Name.ToLower().Contains(specParams.Search)) &&
            (specParams.BrandId == null || p.ProductBrandId == specParams.BrandId) &&
            (specParams.TypeId == null || p.ProductTypeId == specParams.TypeId)
        );

        // pagination
        AddPagination(specParams.PageSize, specParams.PageIndex);
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