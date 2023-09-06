using System.Text.Json.Serialization;

namespace Entities;

public class Portfolio
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    public string UserId { get; set; }
}