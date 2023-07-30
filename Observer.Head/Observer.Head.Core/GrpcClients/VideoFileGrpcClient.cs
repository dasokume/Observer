using Google.Protobuf;
using Grpc.Core;
using GrpcVideo;
using Microsoft.Extensions.Logging;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Observer.Head.Core.Video.CommandHandlers;
using System.IO.Compression;

namespace Observer.Head.Core.Services;

public class VideoFileGrpcClient : IVideoFileGrpcClient
{
    private readonly Channel _channel;
    private readonly ILogger<VideoFileGrpcClient> _logger;

    public VideoFileGrpcClient(ILogger<VideoFileGrpcClient> logger)
    {
        _logger = logger;
        _channel = new("localhost:5099", ChannelCredentials.Insecure);
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        await _channel.ConnectAsync();

        var request = new DeleteFileRequest
        {
            FileName = fileName
        };

        var client = new VideoFileService.VideoFileServiceClient(_channel);

        var response = await client.DeleteFileAsync(request);

        return response != null && response.IsDeleted;
    }

    public async Task SaveFileAsync(Entities.VideoFile video, IProgress<string> progress)
    {
        /*
         * This kind of logic will upload ALL file at once.
         * Very bad for big files -> refactor:
         * https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-7.0#upload-large-files-with-streaming
         */
        using var memoryStream = new MemoryStream();
        await video.File.CopyToAsync(memoryStream);
        var fileContent = memoryStream.ToArray();

        await _channel.ConnectAsync();

        var request = new SaveFileRequest
        {
            FileName = video.FileName,
            FileLength = video.File.Length,
            FileContent = ByteString.CopyFrom(fileContent)
        };

        var client = new VideoFileService.VideoFileServiceClient(_channel);

        var response = client.SaveFile(request);

        while (await response.ResponseStream.MoveNext())
        {
            var current = response.ResponseStream.Current;
            progress.Report(current.PercentComplete);
        }
    }

    public IAsyncEnumerable<BufferedVideo> StreamVideo(string fileName)
    {
        throw new NotImplementedException();
    }
}