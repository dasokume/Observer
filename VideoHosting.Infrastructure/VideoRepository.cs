using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using Microsoft.Azure.Cosmos;

namespace VideoHosting.Infrastructure
{
    public class VideoRepository : IVideoRepository
    {
        private readonly Container _container;
        private readonly CosmosDbContext _cosmosDbContext;

        public VideoRepository(CosmosDbContext сosmosDbContext)
        {
            _cosmosDbContext = сosmosDbContext;
            _container = сosmosDbContext.GetContainer();
        }

        public async Task CreateCosmosDbItemAsync(VideoMetadata videometadata)
        {
            var videoMetadata = new VideoMetadata
            {
                Id = Guid.NewGuid().ToString(),
                FileName = videometadata.FileName,
                Title = videometadata.Title,
                Description = videometadata.Description,
            };

            await _container.CreateItemAsync(videoMetadata, new PartitionKey(videoMetadata.Id));
        }

        public async Task<VideoMetadata> GetVideoIdMetadataAsync(VideoMetadata videometadata)
        {
            var partitionKey = new PartitionKey(videometadata.Id);
            var getVideoById = await _container.ReadItemAsync<VideoMetadata>(videometadata.Id.ToString(), partitionKey);
            return getVideoById;
        }

        public async Task<bool> DeleteCosmosDbItemAsync(VideoMetadata videometadata)
        {
            try
            {
                var id = videometadata.Id.ToString();
                var partitionKey = new PartitionKey(id);
                await _container.DeleteItemAsync<VideoMetadata>(id, partitionKey);
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