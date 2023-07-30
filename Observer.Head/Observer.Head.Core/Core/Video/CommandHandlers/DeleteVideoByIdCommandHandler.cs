using MediatR;
using Observer.Head.Core.Interfaces;
using Observer.Head.Core.Video.Commands;

namespace Observer.Head.Core.Video.CommandHandlers;

public class DeleteVideoByIdCommandHandler : IRequestHandler<DeleteVideoByIdCommand, bool>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IVideoFileGrpcClient _videoFileGrpcClient;

    public DeleteVideoByIdCommandHandler(IVideoRepository videoRepository, IVideoFileGrpcClient videoFileGrpcClient)
    {
        _videoRepository = videoRepository;
        _videoFileGrpcClient = videoFileGrpcClient;
    }

    public async Task<bool> Handle(DeleteVideoByIdCommand request, CancellationToken cancellationToken)
    {
        var videoMetadata = await _videoRepository.GetAsync(request.Id);

        var isFileDeleted = await _videoFileGrpcClient.DeleteFileAsync(videoMetadata.FileName);

        var isMetadataDeleted = await _videoRepository.DeleteAsync(request.Id);

        return isFileDeleted && isMetadataDeleted;
    }
}