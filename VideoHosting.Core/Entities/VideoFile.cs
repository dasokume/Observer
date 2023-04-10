using Microsoft.AspNetCore.Http;

namespace VideoHosting.Core.Entities
{
    public class VideoFile : VideoBase
    {
        public IFormFile File { get; set; }

        public string FileName { get; set; }
    }
}