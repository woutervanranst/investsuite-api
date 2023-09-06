using System.Net;
using Microsoft.Azure.Cosmos;

namespace Entities;

public class UserCosmosDbContext : CosmosDbContext
{
    public UserCosmosDbContext(string accountEndpoint, string accountKey, string databaseName,
        string containerName) : base(accountEndpoint, accountKey, databaseName, containerName)
    {
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        try
        {
            var r = await container.ReadItemAsync<User>(userId, new PartitionKey(userId));
            return r is not null && r.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception e) when (e is CosmosException { StatusCode: HttpStatusCode.NotFound })
        {
            return false;
        }
    }

}