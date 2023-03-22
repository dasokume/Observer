using MediatR;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.VideoManagment.Queries
{
    public record GetVideoByIdQuery(int Id) : IStreamRequest<BufferedVideo>;
}