using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(dist => dist.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dist => dist.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dist => dist.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());
    }
}