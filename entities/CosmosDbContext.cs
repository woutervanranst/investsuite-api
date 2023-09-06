using Microsoft.Azure.Cosmos;

namespace Entities;

public class CosmosDbContext
{
    private readonly CosmosClient cosmosClient;
    private readonly Database     database;
    private readonly Container    container;

    public CosmosDbContext(string accountEndpoint, string accountKey, string databaseName, string containerName)
    {
        cosmosClient = new CosmosClient(accountEndpoint, accountKey);
        database = cosmosClient.GetDatabase(databaseName);
        container = database.GetContainer(containerName);
    }

    public async Task AddItemAsync<T>(T item)
    {
        await container.CreateItemAsync(item);
    }

    public IAsyncEnumerable<Portfolio> GetPortfoliosByUserIdAsync(string userId)
    {
        var query = new QueryDefinition("SELECT * FROM Portfolios p WHERE p.UserId = @userId")
            .WithParameter("@userId", userId);

        return container.GetItemQueryIterator<Portfolio>(query).ToAsyncEnumerable();
    }
}