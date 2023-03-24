using MediatR;

namespace VideoHosting.Core.VideoManagment.Commands
{
    public record DeleteVideoByIdCommand(int Id) : IRequest<bool>;
}