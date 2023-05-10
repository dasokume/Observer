using MediatR;
using VideoHosting.Core.Comments.Queries;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Core.Comments.QueryHandlers;

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