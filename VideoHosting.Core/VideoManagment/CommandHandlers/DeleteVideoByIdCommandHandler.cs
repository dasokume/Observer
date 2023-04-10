using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Commands;

namespace VideoHosting.Core.VideoManagment.CommandHandlers
{
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
            var videoMetadata = await _videoRepository.GetMetadataAsync(request.Id);

            var isFileDeleted = _videoFileRepository.DeleteFileAsync(new VideoFile { FileName = videoMetadata.FileName });

            var isMetadataDeleted = await _videoRepository.DeleteMetadataAsync(request.Id);

            return isFileDeleted && isMetadataDeleted;
        }
    }
}