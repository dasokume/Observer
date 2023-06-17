using System.Linq.Expressions;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Observer.Head.Infrastructure.Constants;

namespace Observer.Head.Infrastructure.Repositories;

public class VideoRepository : BaseRepository<VideoMetadata>, IVideoRepository
{
    public VideoRepository(CosmosDbContext cosmosDbContext) : base(cosmosDbContext, PartitionKeys.VideoMetadataKey)
    {
    }

    public override Task<IList<VideoMetadata>> WhereAsync(Expression<Func<VideoMetadata, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}