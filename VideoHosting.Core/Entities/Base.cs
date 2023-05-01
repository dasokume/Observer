using Newtonsoft.Json;

namespace VideoHosting.Core.Entities;

public class Base
{
    [JsonProperty("id")]
    public string Id { get; set; } = default!;
}