using Microsoft.AspNetCore.Http;

namespace VideoHosting.Core.Entities
{
    public class Video
    {
        public IFormFile File { get; set; }
    }
}