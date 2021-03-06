using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _brandRepo;
    private readonly IGenericRepository<ProductType> _typeRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo,
        IGenericRepository<ProductType> typeRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _brandRepo = brandRepo;
        _typeRepo = typeRepo;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> GetProduct([FromRoute] int id)
    {
        ISpecification<Product> productByIdWithBrandAndType =
            new ProductsWithBrandsAndTypesSpecification(id);

        Product? productOrNull = await _productRepo
            .GetOneWithSpecAsync(productByIdWithBrandAndType);

        if (productOrNull is null) return NotFound(new ApiErrorResponse(404));

        return _mapper.Map<Product, ProductDTO>(productOrNull);
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts(
        [FromQuery] ProductSpecParams specParams)
    {
        ISpecification<Product> productsWithBrandsAndTypes =
            new ProductsWithBrandsAndTypesSpecification(specParams);

        ISpecification<Product> productsWithFilterForCount =
            new ProductWithFiltersForCountSpecification(specParams);

        int productCount = await _productRepo.CountAsync(productsWithFilterForCount);

        IReadOnlyList<Product> products =
            await _productRepo.ListWithSpecAsync(productsWithBrandsAndTypes);

        IReadOnlyList<ProductDTO> data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);

        return new Pagination<ProductDTO>(
            pageIndex: specParams.PageIndex,
            pageSize: specParams.PageSize,
            count: productCount,
            data: data);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
        IReadOnlyList<ProductBrand> brands = await _brandRepo.ListAllAsync();
        return brands.ToList();
    }

    [HttpGet("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        IReadOnlyList<ProductType> types = await _typeRepo.ListAllAsync();
        return types.ToList();
    }
}