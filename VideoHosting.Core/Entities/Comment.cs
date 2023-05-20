﻿using VideoHosting.Infrastructure.Constants;

namespace VideoHosting.Core.Entities;

public record Comment : Base
{
    public string VideoMetadataId { get; init; } = default!;

    public string Text { get; init; } = default!;

    public DateTime CommentDate { get; init; } = DateTime.Now;

    public Comment() : base(PartitionKeys.CommentKey)
    {
    }
}