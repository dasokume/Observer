using System.Linq.Expressions;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Observer.Head.Infrastructure.Constants;

namespace Observer.Head.Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(CosmosDbContext cosmosDbContext) : base(cosmosDbContext, PartitionKeys.CommentKey)
    {
    }

    public override async Task<IList<Comment>> WhereAsync(Expression<Func<Comment, bool>> predicate)
    {
        return await _cosmosDbContext.WhereAsync(predicate);
    }
}