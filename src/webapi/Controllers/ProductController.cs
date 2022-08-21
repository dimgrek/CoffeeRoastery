using System;
using System.Threading.Tasks;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using webapi.Extensions;

namespace webapi.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        return await productService.GetById(productId).ToActionResult();
    }
}