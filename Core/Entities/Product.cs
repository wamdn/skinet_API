using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Product
{
   
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(maximumLength: 255, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
}
