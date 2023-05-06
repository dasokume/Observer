namespace VideoHosting.Infrastructure.Interfaces;

public interface IConfigurationParser
{
    CosmosDbSettings GetCosmosDbSettings();
}