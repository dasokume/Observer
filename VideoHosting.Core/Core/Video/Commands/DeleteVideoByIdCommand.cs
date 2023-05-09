using MediatR;

namespace VideoHosting.Core.Video.Commands;

public record DeleteVideoByIdCommand(string Id) : IRequest<bool>;