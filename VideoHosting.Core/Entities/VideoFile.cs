using Microsoft.AspNetCore.Http;

namespace VideoHosting.Core.Entities;

public class VideoFile : Base
{
    public IFormFile File { get; set; } = default!;

    public string FileName { get; set; } = default!;
}