using System.Net.Http;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IDataAccess
    {
        public IAsyncEnumerable<BufferedVideo> GetVideoById(int id); 
    }
}