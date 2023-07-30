using Grpc.Core;
using GrpcVideo;
using Microsoft.AspNetCore.Http;
using static GrpcVideo.VideoFileService;

namespace Observer.Head.Infrastructure.Repositories;

public class VideoFileGrpcServiceImpl : VideoFileServiceBase
{
    private readonly string _videosDirectory;

    public VideoFileGrpcServiceImpl()
    {
        _videosDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos");
    }

    //public async IAsyncEnumerable<BufferedVideo> StreamVideoAsync(string fileName)
    //{
    //    var bufferSize = 1024 * 1024;
    //    byte[] buffer = new byte[bufferSize];

    //    using var stream = new FileStream(Path.Combine(_videosDirectory, video.FileName), FileMode.Open);
    //    using var bufferedStream = new BufferedStream(stream, bufferSize);
    //    BufferedVideo bufferedVideo = new BufferedVideo { BufferSize = bufferSize };

    //    int bytesRead;
    //    while ((bytesRead = await bufferedStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
    //    {
    //        bufferedVideo.Buffer = buffer;
    //        bufferedVideo.BytesRead = bytesRead;
    //        yield return bufferedVideo;
    //    }
    //}

    public override async Task SaveFile(SaveFileRequest request, IServerStreamWriter<SaveFileResponse> responseStream, ServerCallContext context)
    {
        var videoFilePath = Path.Combine(_videosDirectory, request.FileName);
        if (File.Exists(videoFilePath))
        {
            throw new Exception($"A file with the name { request.FileName } already exists.");
        }

        byte[] buffer = new byte[16 * 1024];

        using FileStream output = new FileStream(videoFilePath, FileMode.Create);
        MemoryStream input = new MemoryStream();
        request.FileContent.WriteTo(input);

        long totalReadBytes = 0;
        int readBytes;

        while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            await output.WriteAsync(buffer, 0, readBytes);
            totalReadBytes += readBytes;

            // Report progress
            var percentComplete = (output.Length / (double)request.FileLength).ToString("0.00%");

            await responseStream.WriteAsync(new SaveFileResponse { PercentComplete = percentComplete });
        }
    }

    public override Task<DeleteFileResponse> DeleteFile(DeleteFileRequest request, ServerCallContext context)
    {
        var response = new DeleteFileResponse();
        try
        {
            response.IsDeleted = true;
            var videoFilePath = Path.Combine(_videosDirectory, request.FileName);

            if (!File.Exists(videoFilePath))
            {
                return Task.FromResult(response);
            }

            File.Delete(videoFilePath);

            return Task.FromResult(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Task.FromResult(response);
        }
    }
}