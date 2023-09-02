using MediatR;
using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Queries.GetComments;

public record GetCommentsQuery(string videoId) : IRequest<List<Comment>>;