namespace VideoHosting.Core.Entities
{
    public class BufferedVideo : VideoBase
    {
        public int BufferSize { get; set; }

        public byte[] Buffer { get; set; }

        public int BytesRead { get; set; }
    }
}