using Grpc.Core;
using GrpcVideo;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;

namespace Observer.Head.Core.Services;

public class VideoFileGrpcClient : IVideoFileGrpcClient
{
    private Channel _channel;

    public VideoFileGrpcClient()
    {
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

    public Task<bool> SaveFileAsync(Entities.VideoFile video, IProgress<string> progress)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<BufferedVideo> StreamVideo(string fileName)
    {
        throw new NotImplementedException();
    }
}