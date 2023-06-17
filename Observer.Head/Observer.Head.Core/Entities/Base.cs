using Newtonsoft.Json;

namespace Observer.Head.Core.Entities;

public abstract record Base
{
    [JsonProperty("id")]
    public string Id { get; init; } = default!;

    [JsonProperty("partitionKey")]
    public string PartitionKey { get; init; } = default!;

    public Base(string partitionKey)
    {
        PartitionKey = partitionKey;
    }
}