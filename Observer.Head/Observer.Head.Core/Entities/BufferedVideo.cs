namespace Observer.Head.Core.Entities;

public record BufferedVideo
{
    public byte[] Buffer { get; set; } = default!;

    public int BytesRead { get; set; } = default!;
}