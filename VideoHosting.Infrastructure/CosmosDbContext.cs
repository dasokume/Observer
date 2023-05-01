using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace VideoHosting.Infrastructure;

public class CosmosDbContext
{
    private readonly ILogger<CosmosDbInitializer> _logger;

    // REFACTOR THIS SHIT
    public Container Tags { get; set; }
    public Container Comments { get; set; }
    private readonly Container _container;

    private readonly CosmosClient _cosmosClient;
    

    public CosmosDbContext(IConfiguration config, ILogger<CosmosDbInitializer> logger)
    {
        _logger = logger;

        var cosmosDbSettings = config.GetSection("CosmosDbSettings");
        _cosmosClient = new CosmosClient(cosmosDbSettings["EndpointUri"], cosmosDbSettings["PrimaryKey"]);
        _container = _cosmosClient.GetContainer(cosmosDbSettings["DatabaseName"], cosmosDbSettings["ContainerName"]);
        Tags = _cosmosClient.GetContainer(cosmosDbSettings["DatabaseName"], "Tags");
        Comments = _cosmosClient.GetContainer(cosmosDbSettings["DatabaseName"], "Comments");
    }

    public async Task<T> ReadItemAsync<T>(string id, string partitionKeyValue)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, GetPartitionKey(partitionKeyValue));

            return GetResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            throw;
        }
    }

    public async Task<T> CreateItemAsync<T>(T item, string partitionKeyValue)
    {
        try
        {
            var response = await _container.CreateItemAsync(item, GetPartitionKey(partitionKeyValue));

            return GetResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteItemAsync<T>(string id, string partitionKeyValue)
    {
        try
        {
            var response = await _container.DeleteItemAsync<T>(id, GetPartitionKey(partitionKeyValue));

            return IsRequestSuccessful(response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteItemIfExistsAsync<T>(string id, string partitionKeyValue)
    {
        var partitionKey = GetPartitionKey(partitionKeyValue);
        var item = await _container.ReadItemAsync<T>(id, partitionKey);

        if (item != null)
        {
            var response = await _container.DeleteItemAsync<T>(id, partitionKey);
            return IsRequestSuccessful(response);
        }

        return true;
    }

    public async Task<T> UpdateItemAsync<T>(T item, string id, string partitionKeyValue)
    {
        try
        {
            var responce = await Tags.ReplaceItemAsync(item, id, GetPartitionKey(partitionKeyValue));
            return GetResult(responce);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            throw;
        }
    }

    private PartitionKey GetPartitionKey(string partitionKeyValue)
    {
        return new PartitionKey(partitionKeyValue);
    }

    private static T GetResult<T>(ItemResponse<T> response)
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
        {
            throw new InvalidOperationException("Invalid status code.");
        }

        return response.Resource;
    }

    private static bool IsRequestSuccessful<T>(ItemResponse<T> response)
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent : return true;
            case HttpStatusCode.OK: return true;
            default: return false;
        }
    }
}