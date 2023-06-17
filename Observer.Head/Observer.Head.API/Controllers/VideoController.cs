using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Observer.Head.API.ViewModels;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Video.Commands;
using Observer.Head.Core.Video.Queries;

namespace Observer.Head.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public VideoController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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
    [Authorize("write:video")]
    public async Task<VideoMetadata> UploadVideo([FromForm] UploadVideoViewModel video)
    {
        var progress = new Progress<string>(percent =>
        {
            Content(percent);
        });

        var uploadVideoCommand = _mapper.Map<UploadVideoCommand>(video);
        uploadVideoCommand.Progress = progress;

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