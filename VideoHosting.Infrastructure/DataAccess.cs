using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHosting.Core.Entities;
using VideoHosting.Core.Interfaces;

namespace VideoHosting.Infrastructure
{
    public class DataAccess : IDataAccess
    {
        public Task<Video> GetVideoById(int id)
        {
            // get file from disk
            throw new NotImplementedException();
        }
    }
}