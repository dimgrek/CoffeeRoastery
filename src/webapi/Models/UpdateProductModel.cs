using System;

namespace webapi.Models;

public class UpdateProductModel : ProductModel
{
    public Guid Id { get; set; }
}