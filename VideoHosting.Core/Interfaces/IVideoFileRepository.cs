﻿using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IVideoFileRepository
    {
        public IAsyncEnumerable<BufferedVideo> StreamVideoAsync(VideoFile video);
        Task<bool> SaveFileAsync(VideoFile video);
        bool DeleteFileAsync(VideoFile video);
    }
}