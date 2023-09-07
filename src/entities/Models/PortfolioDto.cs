using Newtonsoft.Json;

namespace Entities.Models;

public class PortfolioDto
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = $"P{Guid.NewGuid()}".ToUpperInvariant();

    public string UserId { get; set; }
}

