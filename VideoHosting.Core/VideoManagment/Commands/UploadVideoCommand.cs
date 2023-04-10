using MediatR;
using Microsoft.AspNetCore.Http;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.VideoManagment.Commands
{
    public record UploadVideoCommand(IFormFile VideoFile, string Title, string Description) : IRequest<VideoMetadata>;
}