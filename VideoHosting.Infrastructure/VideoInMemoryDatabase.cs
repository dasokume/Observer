using Microsoft.AspNetCore.Http;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure
{
    public class VideoInMemoryDatabase : IVideoInMemoryDatabase
    {
        private static Dictionary<int, string> _videoDatabase = new Dictionary<int, string>
        {
            { 1, "sample.mp4" },
            { 2, "sample1.mp4" },
            { 3, "sample2.mp4" }
        };
        private static int LatestId = 3;
        private const string VideoRelativePath = "bin\\Debug\\net6.0\\Videos\\";

        private string GetVideoStorageDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), VideoRelativePath);
        }

        private string GetVideoFullPath(int id)
        {
            var videoFileName = _videoDatabase[id];

            return Path.Combine(GetVideoStorageDirectory(), videoFileName);
        }

        private string GetNewVideoPath(string videoFileName)
        {
            return Path.Combine(GetVideoStorageDirectory(), videoFileName);
        }

        public async IAsyncEnumerable<BufferedVideo> StreamAsync(VideoBase video)
        {
            int bufferSize = 1024 * 1024; // ~1 MB
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            var videoPath = GetVideoFullPath(video.Id);
            using var stream = new FileStream(videoPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var bufferedStream = new BufferedStream(stream, bufferSize);

            while ((bytesRead = await bufferedStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                byte[] data = new byte[bytesRead];
                Array.Copy(buffer, data, bytesRead);
                yield return new BufferedVideo { BufferSize = bufferSize, Buffer = data, BytesRead = bytesRead };
            }
        }

        public async Task<bool> WriteAsync(Video video)
        {
            var saveVideoPath = GetNewVideoPath(video.VideoFile.FileName);
            using var stream = new FileStream(saveVideoPath, FileMode.Create);
            {
                await video.VideoFile.CopyToAsync(stream);
            }

            LatestId = LatestId + 1;

            _videoDatabase.Add(LatestId, video.VideoFile.FileName);

            return true;
        }

        public bool Delete(VideoBase video)
        {
            string videoFilePath = Path.Combine(Directory.GetCurrentDirectory(), _videoDatabase[video.Id]);

            if (!File.Exists(videoFilePath))
            {
                throw new FileNotFoundException($"Video with ID: {video.Id} not found.");
            }

            File.Delete(videoFilePath);
            _videoDatabase.Remove(video.Id);

            return true;
        }
    }
}