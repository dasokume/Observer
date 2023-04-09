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
            var videoFile = new VideoFile { Id = request.Id.ToString() };
            await _videoFileRepository.DeleteVideoAsync(videoFile);

            var videoMetadata = new VideoMetadata { Id = request.Id.ToString() };
            await _videoRepository.DeleteCosmosDbItemAsync(videoMetadata);

            return true;
        }
    }
}