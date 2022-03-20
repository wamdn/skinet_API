using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
{
    public ProductsWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
    {
        AddIncludes();
    }

    public ProductsWithBrandsAndTypesSpecification()
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.ProductBrand);
        Includes.Add(p => p.ProductType);
    }
}