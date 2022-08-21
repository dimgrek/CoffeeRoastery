using System;

namespace CoffeeRoastery.DAL.Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}