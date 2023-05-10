using Newtonsoft.Json;

namespace VideoHosting.Core.Entities;

public record VideoMetadata : Base
{
    [JsonProperty("fileName")]
    public string FileName { get; init; } = default!;

    [JsonProperty("title")]
    public string Title { get; init; } = default!;

    [JsonProperty("description")]
    public string Description { get; init; } = default!;

    [JsonProperty("tags")]
    public List<Tag>? Tags { get; init; }

    [JsonProperty("Views")]
    public long Views { get; init; } = default!;
}