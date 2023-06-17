namespace Observer.Head.Infrastructure.Interfaces;

public interface IConfigurationParser
{
    CosmosDbSettings GetCosmosDbSettings();
}