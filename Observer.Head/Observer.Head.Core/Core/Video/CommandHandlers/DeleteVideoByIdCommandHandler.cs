using MediatR;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Observer.Head.Core.Video.Commands;

namespace Observer.Head.Core.Video.CommandHandlers;

public class DeleteVideoByIdCommandHandler : IRequestHandler<DeleteVideoByIdCommand, bool>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IVideoFileRepository _videoFileRepository;

    public DeleteVideoByIdCommandHandler(IVideoRepository videoRepository, IVideoFileRepository videoFileRepository)
    {
        _videoRepository = videoRepository;
        _videoFileRepository = videoFileRepository;
    }

    public async Task<bool> Handle(DeleteVideoByIdCommand request, CancellationToken cancellationToken)
    {
        var videoMetadata = await _videoRepository.GetAsync(request.Id);

        var isFileDeleted = _videoFileRepository.DeleteFileAsync(new VideoFile { FileName = videoMetadata.FileName });

        var isMetadataDeleted = await _videoRepository.DeleteAsync(request.Id);

        return isFileDeleted && isMetadataDeleted;
    }
}