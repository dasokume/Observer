using System.Linq.Expressions;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Infrastructure.Constants;

namespace VideoHosting.Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(CosmosDbContext cosmosDbContext) : base(cosmosDbContext, PartitionKeys.VideoCommentsKey)
    {
    }

    public override async Task<IList<Comment>> WhereAsync(Expression<Func<Comment, bool>> predicate)
    {
        return await _cosmosDbContext.WhereAsync(predicate);
    }
}