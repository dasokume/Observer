using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Interfaces;

public interface IVideoFileRepository
{
    public IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoFile video);
    Task<bool> SaveFileAsync(VideoFile video, IProgress<string> progress);
    bool DeleteFileAsync(VideoFile video);
}