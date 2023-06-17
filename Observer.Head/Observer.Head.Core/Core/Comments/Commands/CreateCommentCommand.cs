using MediatR;
using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Comments.Commands;

public record CreateCommentCommand : IRequest<Comment>
{
    public string VideoMetadataId { get; init; } = default!;

    public string Text { get; init; } = default!;
}