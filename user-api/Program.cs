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

var context = new UserCosmosDbContext(cosmosEndpoint, cosmosKey, cosmosDatabaseName, containerName);

app.MapGet("/users/{userId}/exists", async (string userId) =>
{
    var exists = await context.UserExistsAsync(userId);

    if (exists)
        return Results.NoContent();
    else
        return Results.NotFound();
});

app.Run();