using System;

namespace CoffeeRoastery.BLL.Interface.Dto;

public class UpdateProductDto : ProductDto
{
    public Guid Id { get; set; }
}