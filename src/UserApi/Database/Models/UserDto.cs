using Newtonsoft.Json;

namespace UserApi.Database.Models;

public class UserDto
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = $"U{Guid.NewGuid()}".ToUpperInvariant();

    public string FirstName { get; set; }
    public string LastName  { get; set; }
}