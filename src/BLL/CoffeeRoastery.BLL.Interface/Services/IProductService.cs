using System;
using System.Threading.Tasks;
using CoffeeRoastery.BLL.Interface.Common;

namespace CoffeeRoastery.BLL.Interface.Services;

public interface IProductService
{
    Task<Result<ProductResponse>> GetById(Guid productId);
}