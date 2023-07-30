using Grpc.Core;
using GrpcVideo;
using Observer.Head.Infrastructure.Repositories;

namespace Observer.Video;

public class Program
{
    static async Task Main(string[] args)
    {
        Server server = new()
        {
            Ports = { new ServerPort("localhost", 5099, ServerCredentials.Insecure) },
            Services = { VideoFileService.BindService(new VideoFileGrpcServiceImpl()) }
        };

        try
        {
            server.Start();
            Console.WriteLine("Press enter to close the server.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.ToString());
        }
        finally
        {
            if (server != null)
            {
                await server.ShutdownAsync();
            }
        }
    }
}