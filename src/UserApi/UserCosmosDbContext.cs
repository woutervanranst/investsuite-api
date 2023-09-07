using Entities.Models;
using Entities.Utils;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace UserApi;

public class UserCosmosDbContext : CosmosDbContextBase
{
    public UserCosmosDbContext(string accountEndpoint, string accountKey, string databaseName,
        string containerName) : base(accountEndpoint, accountKey, databaseName, containerName)
    {
    }

    public async Task<UserDto> GetUserAsync(string userId)
    {
        var r = await container.ReadItemAsync<UserDto>(userId, new PartitionKey(userId));
        return r.Resource;
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        try
        {
            var r = await container.ReadItemAsync<UserDto>(userId, new PartitionKey(userId));
            return r is not null && r.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception e) when (e is CosmosException { StatusCode: HttpStatusCode.NotFound })
        {
            return false;
        }
    }

}