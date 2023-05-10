using MediatR;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Comments.Commands;

public record CreateCommentCommand : IRequest<Comment>
{
    public string VideoMetadataId { get; init; } = default!;

    public string Text { get; init; } = default!;
}