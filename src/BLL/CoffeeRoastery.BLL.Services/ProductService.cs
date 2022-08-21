using System;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Services;
using CoffeeRoastery.DAL.Interface.Repositories;
using Microsoft.Extensions.Logging;

namespace CoffeeRoastery.BLL.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    private readonly ILogger<ProductService> logger;

    public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<Result<ProductResponse>> GetById(Guid productId)
    {
        logger.LogInformation("{method} called", nameof(GetById));
        try
        {
            var product = await productRepository.GetByIdAsync(productId);
            if (product is null)
            {
                const string message = "Product not found. productId={0}";
                logger.LogWarning(message, productId);
                return Result.Fail<ProductResponse>(message, productId);
            }

            return Result.Ok(mapper.Map<ProductResponse>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {method}, message: {message}", nameof(GetById), ex.Message);
            return Result.Fail<ProductResponse>($"Failed to {nameof(GetById)}");
        }
    }
}