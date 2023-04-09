using Newtonsoft.Json;

namespace VideoHosting.Core.Entities
{
    public class VideoBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}