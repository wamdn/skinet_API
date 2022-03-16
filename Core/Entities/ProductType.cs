using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ProductType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // public List<Product> Products { get; set; }
}