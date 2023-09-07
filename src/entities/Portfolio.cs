using Newtonsoft.Json;

namespace Entities;

public class Portfolio
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = $"P{Guid.NewGuid()}".ToUpperInvariant();

    public string UserId { get; set; }
}

public class User
{
    [JsonProperty(PropertyName = "id")]
    public string Id   { get; set; } = $"U{Guid.NewGuid()}".ToUpperInvariant();

    public string FirstName { get; set; }
    public string LastName { get; set; }
}