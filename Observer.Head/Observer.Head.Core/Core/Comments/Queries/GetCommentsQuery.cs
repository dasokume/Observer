using MediatR;
using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Comments.Queries;

public record GetCommentsQuery (string videoId) : IRequest<List<Comment>>;