using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Interfaces;

public interface IVideoFileGrpcClient
{
    Task<bool> DeleteFileAsync(string fileName);
    IAsyncEnumerable<BufferedVideo> StreamVideo(string fileName);
    Task<bool> SaveFileAsync(VideoFile video, IProgress<string> progress);
}