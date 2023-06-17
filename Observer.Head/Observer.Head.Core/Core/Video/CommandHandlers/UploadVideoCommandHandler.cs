using AutoMapper;
using MediatR;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Observer.Head.Core.Video.Commands;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace Observer.Head.Core.Video.CommandHandlers;

public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, VideoMetadata>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IVideoFileRepository _videoFileRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UploadVideoCommandHandler> _logger;

    public UploadVideoCommandHandler(
        IVideoRepository videoRepository,
        IVideoFileRepository videoFileRepository,
        IMapper mapper,
        ILogger<UploadVideoCommandHandler> logger)
    {
        _videoRepository = videoRepository;
        _videoFileRepository = videoFileRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<VideoMetadata> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var videoMetadata = _mapper.Map<VideoMetadata>(request);

            var parsedFileName = ParseFileName(request.VideoFile.FileName);
            var newFileName = $"{parsedFileName.name}-{videoMetadata.Id}{parsedFileName.extention}";
            videoMetadata.FileName = newFileName;

            var createdResourse = await _videoRepository.CreateAsync(videoMetadata);

            var videoFile = new VideoFile
            {
                File = request.VideoFile,
                VideoMetadataId = videoMetadata.Id,
                FileName = newFileName
            };
            await _videoFileRepository.SaveFileAsync(videoFile, request.Progress);

            return createdResourse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to upload video.");
            throw;
        }
    }

    private static (string name, string extention) ParseFileName(string fileName)
    {
        int lastIndex = fileName.LastIndexOf(".");
        if (lastIndex == -1)
        {
            throw new Exception("Unable to extract file name.");
        }

        var name = fileName.Substring(0, lastIndex);
        var extention = fileName.Substring(lastIndex, fileName.Length - lastIndex);

        return (name, extention);
    }
}