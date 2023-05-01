using MediatR;

namespace VideoHosting.Core.VideoManagment.Commands;

public record DeleteVideoByIdCommand(string Id) : IRequest<bool>;