using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Queries.GetVideo;

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

        [HttpGet(Name = "GetVideoById")]
        public async Task<Video> GetVideoById(int id)
        {
            var video = await _mediator.Send(new GetVideoByIdQuery(id));

            return video;
        }
    }
}