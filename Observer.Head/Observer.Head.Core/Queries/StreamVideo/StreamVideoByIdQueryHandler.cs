using MediatR;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;

namespace Observer.Head.Core.Queries.StreamVideo;

public class StreamVideoByIdQueryHandler : IStreamRequestHandler<StreamVideoByIdQuery, BufferedVideo>
{
    private readonly IVideoFileGrpcClient _videoFileGrpcClient;
    private readonly IVideoRepository _videoRepository;

    public StreamVideoByIdQueryHandler(IVideoFileGrpcClient videoFileGrpcClient, IVideoRepository videoRepository)
    {
        _videoFileGrpcClient = videoFileGrpcClient;
        _videoRepository = videoRepository;
    }

    public IAsyncEnumerable<BufferedVideo> Handle(StreamVideoByIdQuery request, CancellationToken cancellationToken)
    {
        var videoMetadata = _videoRepository.GetAsync(request.Id).GetAwaiter().GetResult();

        return _videoFileGrpcClient.StreamVideo(videoMetadata.FileName);
    }
}