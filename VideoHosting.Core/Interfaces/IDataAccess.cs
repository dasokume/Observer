using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Interfaces
{
    public interface IDataAccess
    {
        public Task<Video> GetVideoById(int id); 
    }
}