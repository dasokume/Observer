using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Queries;

namespace VideoHosting.Core.VideoManagment.QueryHandlers
{
    public record GetVideoByIdQueryHandler : IStreamRequestHandler<GetVideoByIdQuery, BufferedVideo>
    {
        private IDataAccess _dataAccess;

        public GetVideoByIdQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IAsyncEnumerable<BufferedVideo> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            return _dataAccess.GetVideoById(request.Id);
        }
    }
}