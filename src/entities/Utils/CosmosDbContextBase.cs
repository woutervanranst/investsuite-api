using Microsoft.Azure.Cosmos;

namespace Entities.Utils;

public abstract class CosmosDbContextBase
{
    //private readonly CosmosClient cosmosClient;
    //private readonly Database database;
    protected readonly Container container;

    protected CosmosDbContextBase(string accountEndpoint, string accountKey, string databaseName, string containerName)
    {
        var cosmosClient = new CosmosClient(accountEndpoint, accountKey);
        var database = cosmosClient.GetDatabase(databaseName);
        container = database.GetContainer(containerName);
    }
}