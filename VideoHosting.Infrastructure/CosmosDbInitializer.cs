using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace VideoHosting.Infrastructure
{
    public class CosmosDbInitializer 
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly string _containerName;

        public CosmosDbInitializer(IConfiguration config)
        {
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
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}