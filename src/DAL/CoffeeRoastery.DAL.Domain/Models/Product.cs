using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeRoastery.DAL.Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    [MaxLength(255)]
    public string Name { get; set; }
    [MaxLength(60)]
    public string CountryOfOrigin { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}