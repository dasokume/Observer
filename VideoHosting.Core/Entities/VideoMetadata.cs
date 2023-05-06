using Newtonsoft.Json;

namespace VideoHosting.Core.Entities;

public class VideoMetadata : Base
{
    [JsonProperty("fileName")]
    public string FileName { get; set; } = default!;

    [JsonProperty("title")]
    public string Title { get; set; } = default!;

    [JsonProperty("description")]
    public string Description { get; set; } = default!;

    [JsonProperty("tags")]
    public List<Tag>? Tags { get; set; }

    [JsonProperty("Views")]
    public long Views { get; set; } = default!;
}