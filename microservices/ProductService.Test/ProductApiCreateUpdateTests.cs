using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductService.Test;

public class ProductApiCreateUpdateTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiCreateUpdateTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreatedResponse()
    {
        // Arrange
        var newProductId = 4;
        var product = new Product
        {
            Id = newProductId,
            Name = "New Product",
            Price = 2.99m
        };

        // Act
        var response = await _client.PostAsync("/api/products", JsonContent.Create(product));

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseText = await response.Content.ReadAsStringAsync();
        var createdProduct = JsonSerializer.Deserialize<Product>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.Equal(newProductId, createdProduct?.Id);
        Assert.Equal(product.Name, createdProduct?.Name);
        Assert.Equal(product.Price, createdProduct?.Price);

        // Act
        response = await _client.GetAsync($"/api/products/{createdProduct?.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseText = await response.Content.ReadAsStringAsync();
        var getProduct = System.Text.Json.JsonSerializer.Deserialize<Product>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.Equal(product.Name, getProduct?.Name);
        Assert.Equal(product.Price, getProduct?.Price);
    }

    [Fact]
    public async Task UpdateProduct_ExistingId_ReturnsOkResponse()
    {
        // Arrange
        var existingProductId = 1;
        var product = new Product
        {
            Id = existingProductId,
            Name = "Updated Product",
            Price = 1.00m
        };

        // Act
        var response = await _client.PutAsync($"/api/products/{existingProductId}", JsonContent.Create(product));

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseText = await response.Content.ReadAsStringAsync();
        var updatedProduct = System.Text.Json.JsonSerializer.Deserialize<Product>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.Equal(existingProductId, updatedProduct?.Id);
        Assert.Equal(product.Name, updatedProduct?.Name);
        Assert.Equal(product.Price, updatedProduct?.Price);

        // Act
        response = await _client.GetAsync($"/api/products/{updatedProduct?.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseText = await response.Content.ReadAsStringAsync();
        var getProduct = System.Text.Json.JsonSerializer.Deserialize<Product>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.Equal(product.Name, getProduct?.Name);
        Assert.Equal(product.Price, getProduct?.Price);
    }

    [Fact]
    public async Task UpdateProduct_NonExistingId_ReturnsNotFoundResponse()
    {
        // Arrange
        var nonExistingProductId = 999;
        var product = new Product
        {
            Id = nonExistingProductId,
            Name = "Updated Product",
            Price = 100
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/products/{nonExistingProductId}", product);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
