namespace Observer.Head.Core.Entities;

public record BufferedVideo
{
    public int BufferSize { get; set; } = default!;

    public byte[] Buffer { get; set; } = default!;

    public int BytesRead { get; set; } = default!;
}