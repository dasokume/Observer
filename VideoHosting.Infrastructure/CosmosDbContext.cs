using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Net;
using VideoHosting.Core.Entities;

namespace VideoHosting.Infrastructure
{
    public class CosmosDbContext
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDbContext(IConfiguration config)
        {
            var cosmosDbSettings = config.GetSection("CosmosDbSettings");
            _cosmosClient = new CosmosClient(cosmosDbSettings["EndpointUri"], cosmosDbSettings["PrimaryKey"]);
            _container = _cosmosClient.GetContainer(cosmosDbSettings["DatabaseName"], cosmosDbSettings["ContainerName"]);
        }

        public async Task<T> ReadItemAsync<T>(string id, string partitionKeyValue)
        {
            var response = await _container.ReadItemAsync<T>(id, GetPartitionKey(partitionKeyValue));

            return GetResult(response);
        }

        public async Task<T> CreateItemAsync<T>(T item, string partitionKeyValue)
        {
            var response = await _container.CreateItemAsync(item, GetPartitionKey(partitionKeyValue));

            return GetResult(response);
        }

        public async Task<bool> DeleteItemAsync<T>(string id, string partitionKeyValue)
        {
            var response = await _container.DeleteItemAsync<T>(id, GetPartitionKey(partitionKeyValue));

            return IsRequestSuccessful(response);
        }

        private PartitionKey GetPartitionKey(string partitionKeyValue)
        {
            return new PartitionKey(partitionKeyValue);
        }

        private T GetResult<T>(ItemResponse<T> response)
        {
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                throw new InvalidOperationException("Invalid status code.");
            }

            return response.Resource;
        }

        private bool IsRequestSuccessful<T>(ItemResponse<T> response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }
    }
}