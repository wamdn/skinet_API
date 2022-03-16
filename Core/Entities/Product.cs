using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string PictureUrl { get; set; } = string.Empty;

    /* Relations */

    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }


    public int ProductBrandId { get; set; }
    public ProductBrand ProductBrand { get; set; }
}
