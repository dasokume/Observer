using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.API.ViewModels;
using VideoHosting.Core.Comments.Commands;
using VideoHosting.Core.Comments.Queries;

namespace VideoHosting.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CommentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{videoId}")]
    public async Task<List<CommentViewModel>> GetComments(string videoId)
    {
        var comments = await _mediator.Send(new GetCommentsQuery(videoId));
        var commentsViewModel = _mapper.Map<List<CommentViewModel>>(comments);

        return commentsViewModel;
    }

    [HttpPost]
    public async Task<CommentViewModel> CreateComment(CommentViewModel commentViewModel)
    {
        var createCommentCommand = _mapper.Map<CreateCommentCommand>(commentViewModel);
        var comment = await _mediator.Send(createCommentCommand);
        var commentViewModelResult = _mapper.Map<CommentViewModel>(comment);

        return commentViewModelResult;
    }

    [HttpPatch]
    public async Task<CommentViewModel> UpdateComment(CommentViewModel commentViewModel)
    {
        var updateCommentCommand = _mapper.Map<UpdateCommentCommand>(commentViewModel);
        var comment = await _mediator.Send(updateCommentCommand);
        var commentViewModelResult = _mapper.Map<CommentViewModel>(comment);

        return commentViewModelResult;
    }

    [HttpDelete]
    public async Task<bool> DeleteComment(string commentId)
    {
        return await _mediator.Send(new DeleteCommentCommand(commentId));
    }
}