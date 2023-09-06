using Microsoft.Azure.Cosmos;

namespace Entities;

public abstract class CosmosDbContext
{
    private readonly   CosmosClient cosmosClient;
    private readonly   Database     database;
    protected readonly Container    container;

    protected CosmosDbContext(string accountEndpoint, string accountKey, string databaseName, string containerName)
    {
        cosmosClient = new CosmosClient(accountEndpoint, accountKey);
        database = cosmosClient.GetDatabase(databaseName);
        container = database.GetContainer(containerName);
    }
}