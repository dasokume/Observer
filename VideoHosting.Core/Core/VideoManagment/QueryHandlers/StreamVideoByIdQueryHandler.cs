using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Queries;

namespace VideoHosting.Core.VideoManagment.QueryHandlers;

public class StreamVideoByIdQueryHandler : IStreamRequestHandler<StreamVideoByIdQuery, BufferedVideo>
{
    private readonly IVideoFileRepository _videoFileRepository;
    private readonly IVideoRepository _videoRepository;

    public StreamVideoByIdQueryHandler(IVideoFileRepository videoFileRepository, IVideoRepository videoRepository)
    {
        _videoFileRepository = videoFileRepository;
        _videoRepository = videoRepository;
    }

    public IAsyncEnumerable<BufferedVideo> Handle(StreamVideoByIdQuery request, CancellationToken cancellationToken)
    {
        var videoMetadata = _videoRepository.GetAsync(request.Id).GetAwaiter().GetResult();

        var videoFile = new VideoFile { FileName = videoMetadata.FileName };

        return _videoFileRepository.StreamVideoAsync(videoFile);
    }
}