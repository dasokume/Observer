using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IVideoRepository
    {
        public Task CreateCosmosDbItemAsync(VideoMetadata videoMetadata);
        public Task<VideoMetadata> GetVideoIdMetadataAsync(VideoMetadata videoMetadata);
        public Task<bool> DeleteCosmosDbItemAsync(VideoMetadata videoMetadata);
    }
}