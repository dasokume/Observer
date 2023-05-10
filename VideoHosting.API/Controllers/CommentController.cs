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

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{videoId}")]
    public async Task<List<CommentViewModel>> GetComments(string videoId)
    {
        var comments = await _mediator.Send(new GetCommentsQuery(videoId));
        var commentsViewModel = new List<CommentViewModel>();
        
        foreach (var comment in comments)
        {
            commentsViewModel.Add(new CommentViewModel
            {
                Id = comment.Id,
                CommentDate = comment.CommentDate,
                Text = comment.Text,
                VideoMetadataId = comment.VideoMetadataId,
            });
        }

        return commentsViewModel;
    }

    [HttpPost]
    public async Task<CommentViewModel> CreateComment(CommentViewModel commentViewModel)
    {
        var createCommentCommand = new CreateCommentCommand
        {
            Text = commentViewModel.Text,
            VideoMetadataId = commentViewModel.VideoMetadataId,
        };

        var comment = await _mediator.Send(createCommentCommand);

        return new CommentViewModel
        {
            Id = comment.Id,
            VideoMetadataId = comment.VideoMetadataId,
            Text = comment.Text,
            CommentDate = comment.CommentDate
        };
    }

    [HttpPatch]
    public async Task<CommentViewModel> UpdateComment(CommentViewModel commentViewModel)
    {
        var updateCommentCommand = new UpdateCommentCommand
        {
            Text = commentViewModel.Text,
            VideoMetadataId = commentViewModel.VideoMetadataId,
        };

        var comment = await _mediator.Send(updateCommentCommand);

        return new CommentViewModel
        {
            Id = comment.Id,
            VideoMetadataId = comment.VideoMetadataId,
            Text = comment.Text,
            CommentDate = comment.CommentDate
        };
    }

    [HttpDelete]
    public async Task<bool> DeleteComment(string commentId)
    {
        return await _mediator.Send(new DeleteCommentCommand(commentId));
    }
}