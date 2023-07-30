using MediatR;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Observer.Head.Core.Video.Queries;

namespace Observer.Head.Core.Video.QueryHandlers;

public class StreamVideoByIdQueryHandler : IStreamRequestHandler<StreamVideoByIdQuery, BufferedVideo>
{
    private readonly IVideoFileGrpcClient _videoFileService;
    private readonly IVideoRepository _videoRepository;

    public StreamVideoByIdQueryHandler(IVideoFileGrpcClient videoFileService, IVideoRepository videoRepository)
    {
        _videoFileService = videoFileService;
        _videoRepository = videoRepository;
    }

    public IAsyncEnumerable<BufferedVideo> Handle(StreamVideoByIdQuery request, CancellationToken cancellationToken)
    {
        var videoMetadata = _videoRepository.GetAsync(request.Id).GetAwaiter().GetResult();

        return _videoFileService.StreamVideo(videoMetadata.FileName);
    }
}