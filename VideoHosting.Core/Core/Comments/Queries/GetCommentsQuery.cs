using MediatR;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Comments.Queries;

public record GetCommentsQuery (string videoId) : IRequest<List<Comment>>;