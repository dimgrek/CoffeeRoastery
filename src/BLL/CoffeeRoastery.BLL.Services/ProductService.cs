using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using CoffeeRoastery.DAL.Domain.Models;
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
        logger.LogInformation("{method} called for productId={productId}", nameof(GetById), productId);
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
            logger.LogError(ex, "Failed to {method}, message: {message}, productId={productId}", nameof(GetById), ex.Message, productId);
            return Result.Fail<ProductResponse>($"Failed to {nameof(GetById)}");
        }
    }

    public Result<IEnumerable<ProductResponse>> GetAll()
    {
        logger.LogInformation("{method} called", nameof(GetAll));
        try
        {
            var products = productRepository.GetAll();
            
            return Result.Ok(mapper.Map<IEnumerable<ProductResponse>>(products));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {method}, message: {message}", nameof(GetAll), ex.Message);
            return Result.Fail<IEnumerable<ProductResponse>>($"Failed to {nameof(GetAll)}");
        }
    }

    public async Task<Result<ProductResponse>> Create(ProductDto dto)
    {
        logger.LogInformation("{method} called", nameof(Create));
        try
        {
            if (await productRepository.ExistsByName(dto.Name))
            {
                const string message = "Product already exists. name={0}";
                logger.LogWarning(message, dto.Name);
                return Result.Fail<ProductResponse>(message, dto.Name);
            }

            var product = mapper.Map<Product>(dto);
            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();

            return Result.Ok(mapper.Map<ProductResponse>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {method}, message: {message}", nameof(Create), ex.Message);
            return Result.Fail<ProductResponse>($"Failed to {nameof(Create)}");
        }
    }

    public async Task<Result<ProductResponse>> Update(UpdateProductDto dto)
    {
        logger.LogInformation("{method} called", nameof(Update));
        try
        {
            var product = await productRepository.GetByIdAsync(dto.Id);
            if (product is null)
            {
                const string message = "Product not found. productId={0}";
                logger.LogWarning(message, dto.Id);
                return Result.Fail<ProductResponse>(message, dto.Id);
            }
            
            //to enable editing of product found by id if same name was passed
            if (!dto.Name.Equals(product.Name) && await productRepository.ExistsByName(dto.Name))
            {
                const string message = "Product already exists. name={0}";
                logger.LogWarning(message, dto.Name);
                return Result.Fail<ProductResponse>(message, dto.Name);
            }

            product.Name = dto.Name;
            product.CountryOfOrigin = dto.CountryOfOrigin;
            product.UpdatedDate = DateTime.UtcNow;
            
            await productRepository.SaveChangesAsync();

            return Result.Ok(mapper.Map<ProductResponse>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {method}, message: {message}", nameof(Update), ex.Message);
            return Result.Fail<ProductResponse>($"Failed to {nameof(Update)}");
        }
    }

    public async Task<Result> DeleteById(Guid productId)
    {
        logger.LogInformation("{method} called for productId={productId}", nameof(DeleteById), productId);
        try
        {
            var product = await productRepository.GetByIdAsync(productId);
            if (product is null)
            {
                const string message = "Product not found. productId={0}";
                logger.LogWarning(message, productId);
                return Result.Fail<ProductResponse>(message, productId);
            }

            await productRepository.RemoveAsync(product.Id);
            await productRepository.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {method}, message: {message}, productId={productId}", nameof(DeleteById), ex.Message, productId);
            return Result.Fail<ProductResponse>($"Failed to {nameof(DeleteById)}");
        }
    }
}