using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IVideoFileRepository
    {
        public IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoFile video);
        Task<bool> SaveVideoAsync(VideoFile video);
        Task<bool> DeleteVideoAsync(VideoBase video);
    }
}