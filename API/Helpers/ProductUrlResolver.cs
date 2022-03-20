using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
{
    private readonly IConfiguration _config;

    public ProductUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(source.PictureUrl)) 
            return string.Empty;

        return _config["ApiUrl"] + '/' + source.PictureUrl;
    }
}