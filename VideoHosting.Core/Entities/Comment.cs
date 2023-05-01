namespace VideoHosting.Core.Entities;

public class Comment : Base
{
    public string Text { get; set; } = default!;

    public DateTime CommentDate { get; set; } = DateTime.Now;
}