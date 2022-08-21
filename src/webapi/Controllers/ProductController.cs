using System;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using webapi.Extensions;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;
    private readonly IMapper mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        this.productService = productService;
        this.mapper = mapper;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        return await productService.GetById(productId).ToActionResult();
    }
    
    [HttpPost("")]
    public async Task<IActionResult> Create(ProductModel model)
    {
        return await productService.Create(mapper.Map<ProductDto>(model)).ToActionResult();
    }
}