using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace VideoHosting.Infrastructure
{
    public class CosmosDbContext
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly string _containerName;

        public CosmosDbContext(IConfiguration config)
        {
            var cosmosDbSettings = config.GetSection("CosmosDbSettings");
            _cosmosClient = new CosmosClient(cosmosDbSettings["EndpointUri"], cosmosDbSettings["PrimaryKey"]);
            _databaseName = cosmosDbSettings["DatabaseName"];
            _containerName = cosmosDbSettings["ContainerName"];
        }

        public Container GetContainer()
        {
            return _cosmosClient.GetContainer(_databaseName, _containerName);
        }
    }
}