using MediatR;
using Microsoft.AspNetCore.Http;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.VideoManagment.Commands;

public record UploadVideoCommand : IRequest<VideoMetadata>
{
    public IFormFile VideoFile { get; init; } = default!;

    public string Title { get; init; } = default!;

    public string Description { get; init; } = default!;

    public List<Tag>? Tags { get; init; }

    public IProgress<string> Progress { get; init; } = default!;
};