using Microsoft.Azure.Cosmos;

namespace Entities;

public class PortfolioCosmosDbContext : CosmosDbContextBase
{
    public PortfolioCosmosDbContext(string accountEndpoint, string accountKey, string databaseName,
        string containerName) : base(accountEndpoint, accountKey, databaseName, containerName)
    {
    }

    public IAsyncEnumerable<Portfolio> GetPortfoliosByUserIdAsync(string userId)
    {
        var query = new QueryDefinition("SELECT * FROM Portfolios p WHERE p.UserId = @userId")
            .WithParameter("@userId", userId);

        return container.GetItemQueryIterator<Portfolio>(query).ToAsyncEnumerable();
    }

    public async Task<Portfolio> CreatePortfolioAsync(Portfolio p)
    {
        var r = await container.CreateItemAsync(p);
        return r.Resource;
    }
}