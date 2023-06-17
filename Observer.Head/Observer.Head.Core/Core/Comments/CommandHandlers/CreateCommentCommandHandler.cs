using MediatR;
using Observer.Head.Core.Comments.Commands;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;

namespace Observer.Head.Core.Comments.CommandHandlers;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Comment>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid().ToString(),
            VideoMetadataId = request.VideoMetadataId,
            Text = request.Text,
            CommentDate = DateTime.UtcNow,
        };

        return await _commentRepository.CreateAsync(comment);
    }
}