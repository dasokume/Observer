namespace VideoHosting.API.ViewModels
{
    public class UploadVideoViewModel
    {
        public IFormFile File { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}