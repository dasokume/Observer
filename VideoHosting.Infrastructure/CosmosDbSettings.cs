namespace VideoHosting.Infrastructure;

public class CosmosDbSettings
{
    public string EndpointUri { get; set; } = default!;
    public string PrimaryKey { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string ContainerName { get; set; } = default!;
}