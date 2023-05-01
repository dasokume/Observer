﻿namespace VideoHosting.API.ViewModels;

public class UploadVideoViewModel
{
    public IFormFile File { get; set; } = default!;

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;
}