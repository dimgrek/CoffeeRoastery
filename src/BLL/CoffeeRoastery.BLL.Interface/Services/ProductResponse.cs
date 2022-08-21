using System;

namespace CoffeeRoastery.BLL.Interface.Services;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CountryOfOrigin { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}