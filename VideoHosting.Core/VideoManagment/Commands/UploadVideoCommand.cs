using MediatR;
using Microsoft.AspNetCore.Http;

namespace VideoHosting.Core.VideoManagment.Commands
{
    public record UploadVideoCommand(IFormFile VideoFile) : IRequest<bool>;
}