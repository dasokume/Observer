using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace VideoHosting.Core.Entities
{
    public class VideoFile : VideoBase
    {
        public IFormFile File { get; set; }
    }
}