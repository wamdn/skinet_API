using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductSpecParams specParams)
    {
        // filter
        AddCriteria(p =>
            (p.Name.ToLower().Contains(specParams.Search)) &&
            (specParams.BrandId == null || p.ProductBrandId == specParams.BrandId) &&
            (specParams.TypeId == null || p.ProductTypeId == specParams.TypeId)
        );
    }
}