using MediatR;

namespace Observer.Head.Core.Comments.Commands;

public record DeleteCommentCommand(string Id) : IRequest<bool>;