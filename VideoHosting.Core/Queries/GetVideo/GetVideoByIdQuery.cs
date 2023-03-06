using MediatR;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Queries.GetVideo
{
    public record GetVideoByIdQuery(int Id) : IRequest<Video>;
}