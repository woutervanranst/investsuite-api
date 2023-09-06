using Entities;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var portfoliosContext = new CosmosDbContext(cosmosEndpoint, cosmosKey, cosmosDatabaseName, containerName);

app.MapGet("/portfolios", async ([FromQuery] string userId) =>
{
    if (string.IsNullOrWhiteSpace(userId))
    {
        return Results.BadRequest("UserId is required");
    }

    var portfolios = await portfoliosContext.GetPortfoliosByUserIdAsync(userId).ToListAsync();

    return Results.Ok(portfolios);
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
