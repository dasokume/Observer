using MediatR;
using VideoHosting.Core.Comments.Commands;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Core.Comments.CommandHandlers;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        return await _commentRepository.DeleteAsync(request.Id);
    }
}