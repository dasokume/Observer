using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(CosmosDbContext cosmosDbContext) : base(cosmosDbContext)
    {
    }
}