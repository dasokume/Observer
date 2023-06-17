using MediatR;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Observer.Head.Core.Video.Queries;

namespace Observer.Head.Core.Video.QueryHandlers;

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