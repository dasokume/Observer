using MediatR;

namespace Observer.Head.Core.Video.Commands;

public record DeleteVideoByIdCommand(string Id) : IRequest<bool>;