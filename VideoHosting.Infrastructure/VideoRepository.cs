using VideoHosting.Core.Interfaces;
using VideoHosting.Core.Entities;

namespace VideoHosting.Infrastructure
{
    public class VideoRepository : IVideoRepository
    {
        private readonly IVideoInMemoryDatabase _videoInMemoryDatabase;

        public VideoRepository(IVideoInMemoryDatabase videoInMemoryDatabase)
        {
            _videoInMemoryDatabase = videoInMemoryDatabase;
        }

        public IAsyncEnumerable<BufferedVideo> StreamAsync(VideoBase video)
        {
            return _videoInMemoryDatabase.StreamAsync(video);
        }

        public async Task<bool> UploadAsync(Video video)
        {
            var isUploaded = await _videoInMemoryDatabase.WriteAsync(video);

            return isUploaded;
        }

        public bool Delete(VideoBase video)
        {
            var isDeleted = _videoInMemoryDatabase.Delete(video);

            return isDeleted;
        }
    }
}