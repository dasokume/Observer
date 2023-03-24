using Microsoft.AspNetCore.Http;

namespace VideoHosting.Core.Entities
{
    public class Video : VideoBase
    {
        public IFormFile VideoFile { get; set; }
    }
}