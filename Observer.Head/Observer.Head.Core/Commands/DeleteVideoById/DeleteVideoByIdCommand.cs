using MediatR;

namespace Observer.Head.Core.Commands.DeleteVideoById;

public record DeleteVideoByIdCommand(string Id) : IRequest<bool>;