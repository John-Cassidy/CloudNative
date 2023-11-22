using ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();

var app = builder.Build();

// Seed the database with some data
app.Services.SeedProducts();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

// map endpoints
app.MapProductEndpoints();

app.Run();

public partial class Program { }