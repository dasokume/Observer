using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IVideoRepository
    {
        public Task<VideoMetadata> CreateMetadataAsync(VideoMetadata videoMetadata);
        public Task<VideoMetadata> GetMetadataAsync(string id);
        public Task<bool> DeleteMetadataAsync(string id);
    }
}