using Newtonsoft.Json;
using Observer.Head.Infrastructure.Constants;

namespace Observer.Head.Core.Entities;

public record VideoMetadata : Base
{
    [JsonProperty("fileName")]
    public string FileName { get; set; } = default!;

    [JsonProperty("title")]
    public string Title { get; init; } = default!;

    [JsonProperty("description")]
    public string Description { get; init; } = default!;

    [JsonProperty("tags")]
    public List<Tag>? Tags { get; init; }

    [JsonProperty("Views")]
    public long Views { get; init; } = default!;

    public VideoMetadata() : base(PartitionKeys.VideoMetadataKey)
    {
    }
}