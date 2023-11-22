using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductService.Test;

public class ProductApiGetTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiGetTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResponse()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/products");
        var responseText = await response.Content.ReadAsStringAsync();
        var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(responseText);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(3, products?.Count);
    }

    [Fact]
    public async Task GetProductById_ExistingId_ReturnsOkResponse()
    {
        // Arrange
        var existingProductId = 1;

        // Act
        var response = await _client.GetAsync($"/api/products/{existingProductId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProductById_NonExistingId_ReturnsNotFoundResponse()
    {
        // Arrange
        var nonExistingProductId = 999;

        // Act
        var response = await _client.GetAsync($"/api/products/{nonExistingProductId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // You can also write tests for the SeedProducts method and other helper methods if needed.
}