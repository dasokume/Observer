using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.API.ViewModels;
using VideoHosting.Core.Entities;
using VideoHosting.Core.VideoManagment.Commands;
using VideoHosting.Core.VideoManagment.Queries;

namespace VideoHosting.API.Controllers
{
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
        public async Task GetVideoById(VideoBaseViewModel video)
        {
            var videoSequence = _mediator.CreateStream(new StreamVideoByIdQuery(video.Id));

            if (videoSequence is null)
            {
                throw new ArgumentNullException("Wtf");
            }

            await foreach (var bufferedVideo in videoSequence)
            {
                await HttpContext.Response.Body.WriteAsync(bufferedVideo.Buffer, 0, bufferedVideo.BytesRead);
            }

            await HttpContext.Response.Body.FlushAsync();
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo([FromForm] UploadVideoViewModel video)
        {
            var isUploaded = await _mediator.Send(new UploadVideoCommand(video.File));
            return Ok(isUploaded);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoById(VideoBaseViewModel video)
        {
            var isDeleted = await _mediator.Send(new DeleteVideoByIdCommand(video.Id));
            return Ok(isDeleted);
        }
    }
}