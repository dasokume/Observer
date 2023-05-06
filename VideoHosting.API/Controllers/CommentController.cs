using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.API.ViewModels;

namespace VideoHosting.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{videoId}")]
    public async Task<List<CommentViewModel>> GetComments(string videoId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<CommentViewModel> PostComment(CommentViewModel commentViewModel)
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    public async Task<CommentViewModel> EditComment(CommentViewModel commentViewModel)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{videoId}")]
    public async Task<bool> DeleteComment(string commentId)
    {
        throw new NotImplementedException();
    }
}