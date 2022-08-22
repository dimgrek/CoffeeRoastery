using System.ComponentModel.DataAnnotations;

namespace webapi.Models;

public class ProductModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string CountryOfOrigin { get; set; }
}