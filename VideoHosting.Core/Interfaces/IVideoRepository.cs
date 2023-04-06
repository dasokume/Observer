using Microsoft.AspNetCore.Http;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IVideoRepository
    {
        public IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoBase video);
        public Task<bool> UploadVideoAsync(Video video);
        public Task<bool> DeleteVideoAsync(VideoBase video);
    }
}