using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using Microsoft.Azure.Cosmos;

namespace VideoHosting.Infrastructure
{
    public class VideoFileRepository : IVideoFileRepository
    {
        private readonly string _videosDirectory;
        private readonly IVideoRepository _videoFRepository;

        public VideoFileRepository(string videosDirectory = null)
        {
            _videosDirectory = videosDirectory ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos");
        }

        public async IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoFile video)
        {
            var bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];

            using var stream = new FileStream(Path.Combine(_videosDirectory, video.File.FileName), FileMode.Open);
            using var bufferedStream = new BufferedStream(stream, bufferSize);
            BufferedVideo bufferedVideo = new BufferedVideo { BufferSize = bufferSize };

            int bytesRead;
            while ((bytesRead = await bufferedStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                bufferedVideo.Buffer = buffer;
                bufferedVideo.BytesRead = bytesRead;
                yield return bufferedVideo;
            }
        }

        public async Task<bool> SaveVideoAsync(VideoFile video)
        {
            var videoFileName = video.File.FileName;
            var videoFilePath = Path.Combine(_videosDirectory, videoFileName);
            if (File.Exists(videoFilePath))
            {
                throw new Exception($"A file with the name {videoFileName} already exists.");
            }

            using var stream = new FileStream(videoFilePath, FileMode.Create);
            await video.File.CopyToAsync(stream);

            return true;
        }

        public async Task<bool> DeleteVideoAsync(VideoBase video)
        {
            try
            {
                var videoFilePath = Path.Combine(_videosDirectory, video.Id);
                if (File.Exists(videoFilePath))
                {
                    //await _videoRepository.DeleteCosmosDbItemAsync(video.Id);
                    File.Delete(videoFilePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}