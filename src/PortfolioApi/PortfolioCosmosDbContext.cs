using Entities.Database;
using Entities.Models;
using Entities.Utils;
using Microsoft.Azure.Cosmos;

namespace Entities;

public class PortfolioCosmosDbContext : CosmosDbContextBase
{
    public PortfolioCosmosDbContext(string accountEndpoint, string accountKey, string databaseName,
        string containerName) : base(accountEndpoint, accountKey, databaseName, containerName)
    {
    }

    public IAsyncEnumerable<PortfolioDto> GetPortfoliosByUserIdAsync(string userId)
    {
        var query = new QueryDefinition("SELECT * FROM Portfolios p WHERE p.UserId = @userId")
            .WithParameter("@userId", userId);

        return container.GetItemQueryIterator<PortfolioDto>(query).ToAsyncEnumerable();
    }

    public async Task<PortfolioDto> CreatePortfolioAsync(PortfolioDto p)
    {
        var r = await container.CreateItemAsync(p);
        return r.Resource;
    }
}