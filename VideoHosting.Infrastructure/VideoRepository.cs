using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure
{
    public class VideoRepository : IVideoRepository
    {
        private readonly CosmosDbContext _cosmosDbContext;

        public VideoRepository(CosmosDbContext сosmosDbContext)
        {
            _cosmosDbContext = сosmosDbContext;
        }

        public async Task<VideoMetadata> CreateMetadataAsync(VideoMetadata videoMetadata)
        {
            return await _cosmosDbContext.CreateItemAsync(videoMetadata, videoMetadata.Id);
        }

        public async Task<VideoMetadata> GetMetadataAsync(string id)
        {
            return await _cosmosDbContext.ReadItemAsync<VideoMetadata>(id, id);
        }

        public async Task<bool> DeleteMetadataAsync(string id)
        {
            return await _cosmosDbContext.DeleteItemAsync<VideoMetadata>(id, id);
        }
    }
}