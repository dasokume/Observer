using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Queries;

namespace VideoHosting.Core.VideoManagment.QueryHandlers
{
    public class StreamVideoByIdQueryHandler : IStreamRequestHandler<StreamVideoByIdQuery, BufferedVideo>
    {
        private readonly IVideoFileRepository _videoFileRepository;

        public StreamVideoByIdQueryHandler(IVideoFileRepository videoFileRepository)
        {
            _videoFileRepository = videoFileRepository;
        }

        public IAsyncEnumerable<BufferedVideo> Handle(StreamVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var videoFile = new VideoFile { Id = request.Id.ToString() };
            return _videoFileRepository.StreamVideoAsync(videoFile);
        }
    }
}