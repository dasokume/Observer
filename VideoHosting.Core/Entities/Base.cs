using Newtonsoft.Json;

namespace VideoHosting.Core.Entities;

public record Base
{
    [JsonProperty("id")]
    public string Id { get; init; } = default!;
}