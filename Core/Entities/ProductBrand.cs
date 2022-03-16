using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class ProductBrand : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // public List<Product> Products { get; set; }
}