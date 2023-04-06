using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoHosting.Core.Entities
{
    public class VideoMetadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        //public string FileName { get; set; }

        //public string Title { get; set; }

        //public string Description { get; set; }

        //public List<string> Tags { get; set; }

        //public string FilePath { get; set; }
    }
}