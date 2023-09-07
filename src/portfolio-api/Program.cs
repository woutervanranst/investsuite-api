using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("UserApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["UserApiUrl"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var cosmosEndpoint     = builder.Configuration["CosmosEndpoint"];
var cosmosKey          = builder.Configuration["CosmosKey"];
var cosmosDatabaseName = builder.Configuration["CosmosDatabaseName"];
var containerName      = builder.Configuration["CosmosContainerName"];

var context = new PortfolioCosmosDbContext(cosmosEndpoint, cosmosKey, cosmosDatabaseName, containerName);



app.MapGet("/portfolios", async ([FromQuery] string userId) =>
{
    if (string.IsNullOrWhiteSpace(userId))
    {
        return Results.BadRequest("UserId is required");
    }

    var portfolios = await context.GetPortfoliosByUserIdAsync(userId).ToListAsync();

    return Results.Ok(portfolios);
});

app.MapPost("/portfolios", async (PortfolioDto p, IHttpClientFactory clientFactory) =>
{
    // Validate User
    var c = clientFactory.CreateClient("UserApi");
    var r = await c.GetAsync($"/users/{p.UserId}/exists");
    
    if (r.StatusCode == HttpStatusCode.NotFound)
        return Results.BadRequest("User does not exist");

    // Validate Portfolio
    if (p is null)
        return Results.BadRequest("Portfolio is required");

    p = await context.CreatePortfolioAsync(p);

    return Results.Created($"/portfolio/{p.Id}", p);
});

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
