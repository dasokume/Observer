using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure.Repositories;

public class VideoRepository : BaseRepository<VideoMetadata>, IVideoRepository
{
    public VideoRepository(CosmosDbContext cosmosDbContext) : base(cosmosDbContext)
    {
    }
}