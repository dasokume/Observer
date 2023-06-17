using MediatR;
using Microsoft.AspNetCore.Http;
using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Video.Commands;

public record UploadVideoCommand : IRequest<VideoMetadata>
{
    public IFormFile VideoFile { get; init; } = default!;

    public string Title { get; init; } = default!;

    public string Description { get; init; } = default!;

    public List<Tag>? Tags { get; init; }

    public IProgress<string> Progress { get; set; } = default!;
};