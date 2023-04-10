using Newtonsoft.Json;

namespace VideoHosting.Core.Entities
{
    public class VideoMetadata : VideoBase
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}