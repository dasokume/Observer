using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Core.Queries.GetVideo
{
    public record GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, Video>
    {
        private IDataAccess _dataAcces;

        public GetVideoByIdQueryHandler(IDataAccess dataAccess)
        {
            _dataAcces = dataAccess;
        }

        public async Task<Video> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataAcces.GetVideoById(request.Id);
        }
    }
}