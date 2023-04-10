using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Commands;

namespace VideoHosting.Core.VideoManagment.CommandHandlers
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, VideoMetadata>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoFileRepository _videoFileRepository;

        public UploadVideoCommandHandler(IVideoRepository videoRepository, IVideoFileRepository videoFileRepository)
        {
            _videoRepository = videoRepository;
            _videoFileRepository = videoFileRepository;
        }

        public async Task<VideoMetadata> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var videoFile = new VideoFile { File = request.VideoFile };
            var isFileSaved = await _videoFileRepository.SaveFileAsync(videoFile);

            var videoMetadata = new VideoMetadata
            {
                Id = Guid.NewGuid().ToString(),
                FileName = request.VideoFile.FileName,
                Title = request.Title,
                Description = request.Description,
            };

            var createdResourse = await _videoRepository.CreateMetadataAsync(videoMetadata);

            return createdResourse;
        }
    }
}