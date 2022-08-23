using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoffeeRoastery.BLL.Interface.Dto;
using FluentAssertions;
using webapi.Models;
using webapi.Tests.Comparers;
using webapi.Tests.Consts;
using webapi.Tests.Extensions;
using Xunit;

namespace webapi.Tests.Tests;

public class ProductControllerTests : BaseTest
{
    public ProductControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldGetProducts()
    {
        var product = Defaults.Products[0];

        var response = await Client.GetAsync($"products/{product.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var productResponse = await response.BodyAs<ProductResponse>();

        product.Should().BeEquivalentTo(productResponse, options => options.Using(new ProductComparer()));
    }
    
    [Fact]
    public async Task ShouldGetAllProducts()
    {
        var products = Defaults.Products;

        var response = await Client.GetAsync("products/all");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var productResponse = await response.BodyAs<IEnumerable<ProductResponse>>();

        if (productResponse != null)
        {
            var productResponses = productResponse.ToList();
            productResponses.Select(x => x.Id).Should().BeEquivalentTo(products.Select(x => x.Id));
            productResponses.Select(x => x.Name).Should().BeEquivalentTo(products.Select(x => x.Name));
            productResponses.Select(x => x.CountryOfOrigin).Should()
                .BeEquivalentTo(products.Select(x => x.CountryOfOrigin));
        }
    }
    
    [Fact]
    public async Task ShouldCreateProduct()
    {
        var productModel = new ProductModel
        {
            Name = "Huehuetenango",
            CountryOfOrigin = "Guatemala"
        };

        var response = await Client.PostAsJsonAsync("products", productModel);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var productResponse = await response.BodyAs<ProductResponse>();

        productResponse?.Name.Should().Be(productModel.Name);
        productResponse?.CountryOfOrigin.Should().Be(productModel.CountryOfOrigin);
    }
    
    [Fact]
    public async Task ShouldNotCreateDuplicateProduct()
    {
        var productModel = new ProductModel
        {
            Name = Defaults.Products[0].Name,
            CountryOfOrigin = Defaults.Products[0].CountryOfOrigin
        };

        var response = await Client.PostAsJsonAsync("products", productModel);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task ShouldUpdateProduct()
    {
        var productModel = new UpdateProductModel
        {
            Id = Defaults.Products[0].Id,
            Name = "Huehuetenango",
            CountryOfOrigin = "Guatemala"
            
        };
        
        var content = new StringContent(JsonSerializer.Serialize(productModel), Encoding.UTF8, "application/json");

        var response = await Client.PatchAsync("products", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var productResponse = await response.BodyAs<ProductResponse>();

        productResponse?.Id.Should().Be(productModel.Id);
        productResponse?.Name.Should().Be(productModel.Name);
        productResponse?.CountryOfOrigin.Should().Be(productModel.CountryOfOrigin);
    }
    
    [Fact]
    public async Task ShouldNotUpdateProductWithDuplicateName()
    {
        var productModel = new UpdateProductModel
        {
            Id = Defaults.Products[0].Id,
            Name = Defaults.Products[1].Name,
            CountryOfOrigin = "Guatemala"
            
        };
        
        var content = new StringContent(JsonSerializer.Serialize(productModel), Encoding.UTF8, "application/json");

        var response = await Client.PatchAsync("products", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task ShouldDeleteProduct()
    {
        var product = Defaults.Products[0];

        var response = await Client.DeleteAsync($"products/{product.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseAfterDelete = await Client.GetAsync("products/all");

        responseAfterDelete.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await responseAfterDelete.BodyAs<IEnumerable<ProductResponse>>();

        products.Should().NotContain(x => x.Id == product.Id);
    }
}