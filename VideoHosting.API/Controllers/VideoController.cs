using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.API.ViewModels;
using VideoHosting.Core.Entities;
using VideoHosting.Core.VideoManagment.Commands;
using VideoHosting.Core.VideoManagment.Queries;

namespace VideoHosting.API.Controllers;

[ApiController]
[Route("[controller]")]
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

        var createdVideoMetadata = await _mediator.Send(new UploadVideoCommand(video.File, video.Title, video.Description, progress));
        return createdVideoMetadata;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideoById(string id)
    {
        var isDeleted = await _mediator.Send(new DeleteVideoByIdCommand(id));
        return Ok(isDeleted);
    }
}