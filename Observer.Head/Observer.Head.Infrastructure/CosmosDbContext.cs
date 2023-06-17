using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Net;
using Observer.Head.Core.Entities;
using Observer.Head.Infrastructure.Interfaces;

namespace Observer.Head.Infrastructure;

public class CosmosDbContext
{
    private readonly ILogger<CosmosDbInitializer> _logger;
    private readonly Container _container;
    private readonly CosmosClient _cosmosClient;

    public CosmosDbContext(IConfigurationParser configurationParser, ILogger<CosmosDbInitializer> logger)
    {
        _logger = logger;
        var cosmosDbSettings = configurationParser.GetCosmosDbSettings();
        _cosmosClient = new CosmosClient(cosmosDbSettings.EndpointUri, cosmosDbSettings.PrimaryKey);
        _container = _cosmosClient.GetContainer(cosmosDbSettings.DatabaseName, cosmosDbSettings.ContainerName);
    }

    public async Task<T> ReadAsync<T>(string id, string partitionKeyValue) where T : Base
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

    public async Task<IList<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate) where T : Base
    {
        try
        {
            using FeedIterator<T> setIterator = _container.GetItemLinqQueryable<T>()
                .Where(predicate)
                .ToFeedIterator();

            var entities = new List<T>();

            while (setIterator.HasMoreResults)
            {
                foreach (var item in await setIterator.ReadNextAsync())
                {
                    entities.Add(item);
                }
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            throw;
        }
    }

    public async Task<T> CreateAsync<T>(T item) where T : Base
    {
        try
        {
            var response = await _container.CreateItemAsync(item, GetPartitionKey(item.PartitionKey));

            return GetResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync<T>(string id, string partitionKeyValue) where T : Base
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

    public async Task<bool> DeleteIfExistsAsync<T>(string id, string partitionKeyValue) where T : Base
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

    public async Task<T> UpdateAsync<T>(T item, string id) where T : Base
    {
        try
        {
            var responce = await _container.ReplaceItemAsync(item, id, GetPartitionKey(item.PartitionKey));
            return GetResult(responce);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            throw;
        }
    }

    private static PartitionKey GetPartitionKey(string partitionKeyValue)
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