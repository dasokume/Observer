using MediatR;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;

namespace Observer.Head.Core.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Comment>
    {
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                VideoMetadataId = request.VideoMetadataId,
                Text = request.Text,
                CommentDate = DateTime.UtcNow,
            };

            return await _commentRepository.UpdateAsync(comment, comment.Id);
        }
    }
}