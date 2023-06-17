namespace Observer.Head.API.ViewModels;

public class CommentViewModel
{
    public string Id { get; set; } = default!;

    public string VideoMetadataId { get; set; } = default!;

    public string Text { get; set; } = default!;

    public DateTime CommentDate { get; set; } = DateTime.Now;
}