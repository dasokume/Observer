using Microsoft.AspNetCore.Http;

namespace VideoHosting.Core.Entities;

public record VideoFile
{
    public string VideoMetadataId { get; init; } = default!;

    public IFormFile File { get; init; } = default!;

    public string FileName { get; init; } = default!;
}