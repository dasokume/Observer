using MediatR;

namespace Observer.Head.Core.Commands.DeleteComment;

public record DeleteCommentCommand(string Id) : IRequest<bool>;