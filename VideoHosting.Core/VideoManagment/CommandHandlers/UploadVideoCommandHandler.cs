using MediatR;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;
using VideoHosting.Core.VideoManagment.Commands;

namespace VideoHosting.Core.VideoManagment.CommandHandlers
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, bool>
    {
        private readonly IVideoRepository _videoRepository;

        public UploadVideoCommandHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<bool> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var convertedRequest = new Video { VideoFile = request.VideoFile };
            return await _videoRepository.UploadAsync(convertedRequest);
        }
    }
}