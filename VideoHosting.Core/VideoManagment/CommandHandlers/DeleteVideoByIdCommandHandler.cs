using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Commands;

namespace VideoHosting.Core.VideoManagment.CommandHandlers
{
    public class DeleteVideoByIdCommandHandler : IRequestHandler<DeleteVideoByIdCommand, bool>
    {
        private readonly IVideoRepository _videoRepository;

        public DeleteVideoByIdCommandHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<bool> Handle(DeleteVideoByIdCommand request, CancellationToken cancellationToken)
        {
            var convertedRequest = new VideoBase { Id = request.Id };
            return await _videoRepository.DeleteVideoAsync(convertedRequest);
        }
    }
}