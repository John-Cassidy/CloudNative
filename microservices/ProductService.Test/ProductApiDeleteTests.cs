using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductService.Test;

public class ProductApiDeleteTests : IClassFixture<WebApplicationFactory<Program>>
{

    private readonly HttpClient _client;

    public ProductApiDeleteTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeleteProduct_ExistingId_ReturnsOkResponse()
    {
        // Arrange
        var existingProductId = 1;

        // Act
        var response = await _client.DeleteAsync($"/api/products/{existingProductId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Act
        response = await _client.GetAsync($"/api/products/{existingProductId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_NonExistingId_ReturnsNotFoundResponse()
    {
        // Arrange
        var nonExistingProductId = 999;

        // Act
        var response = await _client.DeleteAsync($"/api/products/{nonExistingProductId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
