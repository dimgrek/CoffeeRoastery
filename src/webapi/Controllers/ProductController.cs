using System;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Extensions;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Authorize]
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
    public async Task<IActionResult> GetById(Guid productId)
    {
        return await productService.GetById(productId).ToActionResult();
    }
    
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        return productService.GetAll().ToActionResult();
    }
    
    [HttpPost("")]
    public async Task<IActionResult> Create(ProductModel model)
    {
        return await productService.Create(mapper.Map<ProductDto>(model)).ToActionResult();
    }
    
    [HttpPatch("")]
    public async Task<IActionResult> Update(UpdateProductModel model)
    {
        return await productService.Update(mapper.Map<UpdateProductDto>(model)).ToActionResult();
    }
    
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteById(Guid productId)
    {
        return await productService.DeleteById(productId).ToActionResult();
    }
}