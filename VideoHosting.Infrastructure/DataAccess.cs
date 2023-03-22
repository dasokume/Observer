using MediatR;
using System.IO;
using System.Net.Mime;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.Entities;
using System.Collections;

namespace VideoHosting.Infrastructure
{
    public class DataAccess : IDataAccess
    {
        private const string Filepath = "bin\\Debug\\net6.0\\Videos\\";
        private static Dictionary<int, string> _videoDatabase = new Dictionary<int, string>
        {
            { 1, Filepath + "sample.mp4" },
            { 2, Filepath + "sample1.mp4" },
            { 3, Filepath + "sample2.mp4" }
        };

        public async IAsyncEnumerable<BufferedVideo> GetVideoById(int id)
        {
            int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            string videoFilePath = Path.Combine(Directory.GetCurrentDirectory(), _videoDatabase[id]);
            using var stream = new FileStream(videoFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var bufferedStream = new BufferedStream(stream, bufferSize);

            while ((bytesRead = await bufferedStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                byte[] data = new byte[bytesRead];
                Array.Copy(buffer, data, bytesRead);
                yield return new BufferedVideo { bufferSize = bufferSize, buffer = data, bytesRead = bytesRead };
            }
        }
    }
}