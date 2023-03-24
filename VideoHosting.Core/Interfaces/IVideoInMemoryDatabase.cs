using Microsoft.AspNetCore.Http;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IVideoInMemoryDatabase
    {
        public IAsyncEnumerable<BufferedVideo> StreamAsync(VideoBase video);

        public Task<bool> WriteAsync(Video video);

        public bool Delete(VideoBase video);
    }
}