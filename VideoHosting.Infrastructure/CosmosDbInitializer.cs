using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VideoHosting.Infrastructure;

public class CosmosDbInitializer 
{
    private readonly ILogger<CosmosDbInitializer> _logger;
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;
    private readonly string _containerName;

    public CosmosDbInitializer(IConfiguration config, ILogger<CosmosDbInitializer> logger)
    {
        _logger = logger;
        var cosmosDbSettings = config.GetSection("CosmosDbSettings");

        _cosmosClient = new CosmosClient(cosmosDbSettings["EndpointUri"], cosmosDbSettings["PrimaryKey"]);
        _databaseName = cosmosDbSettings["DatabaseName"];
        _containerName = cosmosDbSettings["ContainerName"];
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
            var database = _cosmosClient.GetDatabase(_databaseName);
            await database.CreateContainerIfNotExistsAsync(_containerName, "/id");
            _logger.LogInformation("Database was initialized");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Failed to connect to CosmosDB: {ex.Message}");
            throw;
        }
    }
}