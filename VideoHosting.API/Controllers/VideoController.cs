using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using VideoHosting.Core.Entities;
using VideoHosting.Core.VideoManagment.Queries;
using VideoHosting.Infrastructure;

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
        public async Task GetVideoById(int id)
        {
            var request = new GetVideoByIdQuery(id);
            var videoSequence = _mediator.CreateStream(request);

            if (videoSequence is null)
            {
                throw new ArgumentNullException("Wtf");
            }

            await foreach (var bufferedVideo in videoSequence)
            {
                await HttpContext.Response.Body.WriteAsync(bufferedVideo.buffer, 0, bufferedVideo.bytesRead);
            }

            await HttpContext.Response.Body.FlushAsync();
        }

        [HttpPost]
        public async Task CreateVideo([FromForm] Video video)
        {
            await Task.Delay(10);
        }
    }
}