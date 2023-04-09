using MediatR;
using Microsoft.AspNetCore.Http;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Commands;

namespace VideoHosting.Core.VideoManagment.CommandHandlers
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, bool>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoFileRepository _videoFileRepository;

        public UploadVideoCommandHandler(IVideoRepository videoRepository, IVideoFileRepository videoFileRepository)
        {
            _videoRepository = videoRepository;
            _videoFileRepository = videoFileRepository;
        }

        public async Task<bool> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var videoFile = new VideoFile { File = request.VideoFile };
            await _videoFileRepository.SaveVideoAsync(videoFile);

            var videoMetadata = new VideoMetadata { FileName = request.VideoFile.FileName };
            await _videoRepository.CreateCosmosDbItemAsync(videoMetadata);

            return true;
        }
    }
}