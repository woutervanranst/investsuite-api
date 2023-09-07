using Entities.Utils;
using Microsoft.Azure.Cosmos;
using System.Net;
using UserApi.Database.Models;

namespace UserApi.Database;

public class UserCosmosDbContext : CosmosDbContextBase
{
    public UserCosmosDbContext(string accountEndpoint, string accountKey, string databaseName,
        string containerName) : base(accountEndpoint, accountKey, databaseName, containerName)
    {
    }

    /// <summary>
    /// Returns null if user does not exist
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<UserDto?> GetUserAsync(string userId)
    {
        try
        {
            var r = await container.ReadItemAsync<UserDto>(userId, new PartitionKey(userId));
            return r.Resource;
        }
        catch (Exception e) when (e is CosmosException { StatusCode: HttpStatusCode.NotFound })
        {
            return null;
        }
    }
}