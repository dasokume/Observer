namespace VideoHosting.Core.Entities;

public class BufferedVideo
{
    public int BufferSize { get; set; } = default!;

    public byte[] Buffer { get; set; } = default!;

    public int BytesRead { get; set; } = default!;
}