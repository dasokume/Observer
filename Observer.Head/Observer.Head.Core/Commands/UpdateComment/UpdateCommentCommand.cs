using MediatR;
using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Commands.UpdateComment;

public record UpdateCommentCommand : IRequest<Comment>
{
    public string VideoMetadataId { get; init; } = default!;

    public string Text { get; init; } = default!;
}