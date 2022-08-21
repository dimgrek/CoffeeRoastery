using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Dto;

namespace CoffeeRoastery.BLL.Interface.Services;

public interface IProductService
{
    Task<Result<ProductResponse>> GetById(Guid productId);
    Result<IEnumerable<ProductResponse>> GetAll();
    Task<Result<ProductResponse>> Create(ProductDto dto);
    Task<Result<ProductResponse>> Update(UpdateProductDto dto);
    Task<Result> DeleteById(Guid productId);
}