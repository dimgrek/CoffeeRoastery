using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    
    /// <summary>
    /// Endpoint to get product by given id
    /// </summary>
    /// <remarks>
    /// Allows to get existing product
    /// </remarks>
    /// <param name="productId">Existing product id </param>
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid productId)
    {
        return await productService.GetById(productId).ToActionResult();
    }
    
    /// <summary>
    /// Endpoint to get all existing products
    /// </summary>
    /// <remarks>
    /// Allows to get all existing products
    /// </remarks>
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        return productService.GetAll().ToActionResult();
    }
    
    /// <summary>
    /// Endpoint to create new product
    /// </summary>
    /// <remarks>
    /// Allows to create new product. Unique name should be provided otherwise, get 400 response
    /// </remarks>
    [HttpPost("")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(ProductModel model)
    {
        return await productService.Create(mapper.Map<ProductDto>(model)).ToActionResult();
    }
    
    /// <summary>
    /// Endpoint to update existing product
    /// </summary>
    /// <remarks>
    /// Allows to update existing product. Unique name should be provided otherwise, get 400 response
    /// </remarks>
    [HttpPatch("")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(UpdateProductModel model)
    {
        return await productService.Update(mapper.Map<UpdateProductDto>(model)).ToActionResult();
    }
    
    /// <summary>
    /// Endpoint to delete product by given id
    /// </summary>
    /// <remarks>
    /// Allows to delet existing product
    /// </remarks>
    /// <param name="productId">Existing product id </param>
    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteById(Guid productId)
    {
        return await productService.DeleteById(productId).ToActionResult();
    }
}