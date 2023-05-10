using MediatR;

namespace VideoHosting.Core.Comments.Commands;

public record DeleteCommentCommand(string Id) : IRequest<bool>;