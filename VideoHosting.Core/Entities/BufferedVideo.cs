using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoHosting.Core.Entities
{
    public class BufferedVideo
    {
        public int bufferSize { get; set; }

        public byte[] buffer { get; set; }

        public int bytesRead { get; set; }
    }
}