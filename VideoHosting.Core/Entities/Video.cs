using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace VideoHosting.Core.Entities
{
    public class Video : VideoBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Tags { get; set; }

        public IFormFile VideoFile { get; set; }

        public string FilePath { get; set; }
    }
}