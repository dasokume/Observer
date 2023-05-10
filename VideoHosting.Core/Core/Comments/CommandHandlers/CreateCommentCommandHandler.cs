﻿using MediatR;
using VideoHosting.Core.Comments.Commands;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Core.Comments.CommandHandlers;

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