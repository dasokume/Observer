namespace VideoHosting.API.ViewModels;

public class UploadVideoViewModel
{
    public IFormFile File { get; set; } = default!;

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public List<string> Tags { get; set; }

    public UploadVideoViewModel()
    {
        Tags = new List<string>();
    }
}