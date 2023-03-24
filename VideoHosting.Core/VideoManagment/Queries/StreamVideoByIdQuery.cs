using MediatR;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.VideoManagment.Queries
{
    public record StreamVideoByIdQuery(int Id) : IStreamRequest<BufferedVideo>;
}