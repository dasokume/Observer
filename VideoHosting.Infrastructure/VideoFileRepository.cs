using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure
{
    public class VideoFileRepository : IVideoFileRepository
    {
        private readonly string _videosDirectory;

        public VideoFileRepository()
        {
            _videosDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos");
        }

        public async IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoFile video)
        {
            var bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];

            using var stream = new FileStream(Path.Combine(_videosDirectory, video.FileName), FileMode.Open);
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

        public async Task<bool> SaveFileAsync(VideoFile video)
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

        public bool DeleteFileAsync(VideoFile video)
        {
            var videoFilePath = Path.Combine(_videosDirectory, video.FileName);

            if (!File.Exists(videoFilePath))
            {
                return true;
            }

            File.Delete(videoFilePath);

            return true;
        }
    }
}