using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Infrastructure.Constants;

namespace VideoHosting.Infrastructure.Repositories;

public class VideoRepository : BaseRepository<VideoMetadata>, IVideoRepository
{
    public VideoRepository(CosmosDbContext cosmosDbContext) : base(cosmosDbContext, PartitionKeys.VideoMetadataKey)
    {
    }
}