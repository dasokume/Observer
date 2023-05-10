using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.API.ViewModels;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Video.Commands;
using VideoHosting.Core.Video.Queries;

namespace VideoHosting.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    private readonly IMediator _mediator;

    public VideoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task GetVideoById(string id)
    {
        var videoSequence = _mediator.CreateStream(new StreamVideoByIdQuery(id));

        await foreach (var bufferedVideo in videoSequence)
        {
            await HttpContext.Response.Body.WriteAsync(bufferedVideo.Buffer, 0, bufferedVideo.BytesRead);
        }

        await HttpContext.Response.Body.FlushAsync();
    }

    [HttpPost]
    public async Task<VideoMetadata> UploadVideo([FromForm] UploadVideoViewModel video)
    {
        var progress = new Progress<string>(percent =>
        {
            Content(percent);
        });

        var uploadVideoCommand = new UploadVideoCommand
        {
            VideoFile = video.File,
            Title = video.Title,
            Description = video.Description,
            Tags = video.Tags?.Select(x => new Tag { Name = x }).ToList(),
            Progress = progress
        };

        var createdVideoMetadata = await _mediator.Send(uploadVideoCommand);
        return createdVideoMetadata;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideoById(string id)
    {
        var isDeleted = await _mediator.Send(new DeleteVideoByIdCommand(id));
        return Ok(isDeleted);
    }
}