namespace ProductService;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add services List<Product> to the container.
        services.AddSingleton<List<Product>>();
        return services;
    }

    public static void SeedProducts(this IServiceProvider services)
    {
        List<Product> products = services.GetRequiredService<List<Product>>();
        products.AddRange(new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Price = 12.99m },
            new Product { Id = 2, Name = "Product B", Price = 23.99m },
            new Product { Id = 3, Name = "Product C", Price = 34.99m },
        });
    }
}
