using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Http;

namespace VideoHosting.Infrastructure
{
    public class CosmosDbVideoRepository : IVideoRepository
    {
        private readonly Container _container;
        private readonly CosmosDbContext _cosmosDbContext;

        public CosmosDbVideoRepository(CosmosDbContext сosmosDbContext)
        {
            _cosmosDbContext = сosmosDbContext;
            _container = сosmosDbContext.GetContainer();
        }

        public async IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoBase video)
        {
            var query = new QueryDefinition("SELECT * FROM Videos WHERE Video.id = @id").WithParameter("@id", video.Id);

            var iterator = _container.GetItemQueryStreamIterator(query);
            var bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            BufferedVideo bufferedVideo = null;

            try
            {
                while (iterator.HasMoreResults)
                {
                    using var response = await iterator.ReadNextAsync();
                    using var stream = response.Content;
                    using var bufferedStream = new BufferedStream(stream, bufferSize);

                    while ((bytesRead = await bufferedStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] data = new byte[bytesRead];
                        Array.Copy(buffer, data, bytesRead);
                        bufferedVideo = new BufferedVideo { BufferSize = bufferSize, Buffer = data, BytesRead = bytesRead };
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            if (bufferedVideo != null)
            {
                yield return bufferedVideo;
            }
        }

        public async Task<bool> SaveVideoAsync(Video video)
        {
            var videosDirName = "Videos";
            var videoFileName = Path.GetFileName(video.VideoFile.FileName);
            var videoFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, videosDirName, videoFileName);
            using var stream = new FileStream(videoFilePath, FileMode.Create);
            await video.VideoFile.OpenReadStream().CopyToAsync(stream);

            return true;
        }

        public async Task CreateCosmosDbItem(VideoMetadata videoMetadata)
        {
            await _container.CreateItemAsync(videoMetadata, new PartitionKey(videoMetadata.Id));
        }

        public async Task<bool> UploadVideoAsync(Video video)
        {
            var videoMetadata = new VideoMetadata
            {
                Id = Guid.NewGuid().ToString(),
                //FileName = video.FileName,
                //Title = video.Title,
                //Description = video.Description,
                //Tags = video.Tags,
                //FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos", $"{video.FileName}")
            };

            await CreateCosmosDbItem(videoMetadata);
            await SaveVideoAsync(video);
            return true;
        }

        public async Task<bool> DeleteVideoAsync(VideoBase video)
        {
            try
            {
                var id = video.Id.ToString();
                var partitionKey = new PartitionKey(id);
                await _container.DeleteItemAsync<Video>(id, partitionKey);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}