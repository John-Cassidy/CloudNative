using ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();

// Add the following for Kubernetes Deployment
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";
builder.WebHost.UseUrls(url);

var app = builder.Build();

// Configure logging
var logLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");
var apiKey = Environment.GetEnvironmentVariable("API_KEY");

// Seed the database with some data
app.Services.SeedProducts();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

// map endpoints
app.MapProductEndpoints();

app.Run();

public partial class Program { }