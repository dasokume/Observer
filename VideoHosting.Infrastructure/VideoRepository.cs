using VideoHosting.Core.Interfaces;
using VideoHosting.Core.Entities;

namespace VideoHosting.Infrastructure
{
    public class VideoRepository : IVideoRepository
    {
        private readonly IVideoRepository _videoRepository;

        public VideoRepository(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoBase video)
        {
            return _videoRepository.StreamVideoAsync(video);
        }

        public async Task<bool> UploadVideoAsync(Video video)
        {
            var isUploaded = await _videoRepository.UploadVideoAsync(video);

            return isUploaded;
        }

        public async Task<bool> DeleteVideoAsync(VideoBase video)
        {
            var isDeleted = await _videoRepository.DeleteVideoAsync(video);

            return isDeleted;
        }
    }
}