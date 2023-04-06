using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Queries;

namespace VideoHosting.Core.VideoManagment.QueryHandlers
{
    public record StreamVideoByIdQueryHandler : IStreamRequestHandler<StreamVideoByIdQuery, BufferedVideo>
    {
        private IVideoRepository _videoRepository;

        public StreamVideoByIdQueryHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public IAsyncEnumerable<BufferedVideo> Handle(StreamVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var convertedRequest = new VideoBase { Id = request.Id };
            return _videoRepository.StreamVideoAsync(convertedRequest);
        }
    }
}