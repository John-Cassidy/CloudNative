namespace ProductService;

public static class MapEndpoints
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/api/products", (List<Product> products) =>
            {
                return Results.Ok(products);
            })
            .WithName("GetProducts")
            .WithOpenApi();

        app.MapGet("/api/products/{id}", (int id, List<Product> products) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(product);
            })
            .WithName("GetProductById")
            .WithOpenApi();

        app.MapPost("/api/products", (Product product, List<Product> products) =>
            {
                products.Add(product);
                return Results.Created($"/api/products/{product.Id}", product);
            })
            .WithName("CreateProduct")
            .WithOpenApi();

        app.MapPut("/api/products/{id}", (int id, Product product, List<Product> products) =>
            {
                var existingProduct = products.FirstOrDefault(p => p.Id == id);
                if (existingProduct is null)
                {
                    return Results.NotFound();
                }
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                return Results.Ok(existingProduct);
            })
            .WithName("UpdateProduct")
            .WithOpenApi();

        app.MapDelete("/api/products/{id}", (int id, List<Product> products) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product is null)
                {
                    return Results.NotFound();
                }
                products.Remove(product);
                return Results.Ok();
            })
            .WithName("DeleteProduct")
            .WithOpenApi();

        return app;
    }
}
