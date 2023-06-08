using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using VideoHosting.Infrastructure.Constants;
using VideoHosting.Infrastructure.Interfaces;

namespace VideoHosting.Infrastructure;

public class CosmosDbInitializer 
{
    private readonly ILogger<CosmosDbInitializer> _logger;
    private readonly CosmosDbSettings _cosmosDbSettings;
    private readonly CosmosClient _cosmosClient;

    public CosmosDbInitializer(IConfigurationParser configurationParser, ILogger<CosmosDbInitializer> logger)
    {
        _logger = logger;
        _cosmosDbSettings = configurationParser.GetCosmosDbSettings();
        _cosmosClient = new CosmosClient(_cosmosDbSettings.EndpointUri, _cosmosDbSettings.PrimaryKey);
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _cosmosClient.CreateDatabaseIfNotExistsAsync(_cosmosDbSettings.DatabaseName);
            var database = _cosmosClient.GetDatabase(_cosmosDbSettings.DatabaseName);
            await database.CreateContainerIfNotExistsAsync(_cosmosDbSettings.ContainerName, PartitionKeys.PartitionKeyPath);
            _cosmosClient.Dispose();

            _logger.LogInformation("Database was initialized");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Failed to connect to CosmosDB: {ex.Message}");
            throw;
        }
    }
}