using MediatR;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;

namespace Observer.Head.Core.Queries.GetComments;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<Comment>>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<List<Comment>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        return (await _commentRepository.WhereAsync(x => x.VideoMetadataId == request.videoId)).ToList();
    }
}