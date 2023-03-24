using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IVideoRepository
    {
        public IAsyncEnumerable<BufferedVideo> StreamAsync(VideoBase video);
        public Task<bool> UploadAsync(Video video);
        public bool Delete(VideoBase video);
    }
}