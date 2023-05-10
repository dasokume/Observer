using Microsoft.AspNetCore.Http;

namespace VideoHosting.Core.Entities;

public record VideoFile : Base
{
    public IFormFile File { get; init; } = default!;

    public string FileName { get; init; } = default!;
}