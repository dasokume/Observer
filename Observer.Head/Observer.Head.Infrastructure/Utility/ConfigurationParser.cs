using Microsoft.Extensions.Configuration;
using Observer.Head.Infrastructure.Interfaces;

namespace Observer.Head.Infrastructure.Utility;

public class ConfigurationParser : IConfigurationParser
{
    private readonly IConfiguration _configuration;

    public ConfigurationParser(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public CosmosDbSettings GetCosmosDbSettings()
    {
        var cosmosDbSection = _configuration.GetSection("CosmosDbSettings");

        return new CosmosDbSettings
        {
            PrimaryKey = cosmosDbSection.GetValue<string>("PrimaryKey"),
            EndpointUri = cosmosDbSection.GetValue<string>("EndpointUri"),
            DatabaseName = cosmosDbSection.GetValue<string>("DatabaseName"),
            ContainerName = cosmosDbSection.GetValue<string>("ContainerName")
        };
    }
}