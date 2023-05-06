using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Commands;

namespace VideoHosting.Core.VideoManagment.CommandHandlers;

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
        var videoId = Guid.NewGuid().ToString();
        var videoFile = new VideoFile { File = request.VideoFile };

        try
        {
            var videoMetadata = new VideoMetadata
            {
                Id = videoId,
                FileName = request.VideoFile.FileName,
                Title = request.Title,
                Description = request.Description,
                Tags = request.Tags
            };

            var createdResourse = await _videoRepository.CreateAsync(videoMetadata);

            var isFileSaved = await _videoFileRepository.SaveFileAsync(videoFile, request.Progress);

            return createdResourse;
        }
        catch (Exception)
        {
            await _videoRepository.DeleteIfExistsAsync(videoId);
            _videoFileRepository.DeleteFileAsync(videoFile);
            throw;
        }
    }
}