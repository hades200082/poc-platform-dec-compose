namespace Infrastructure.Cosmos;

internal sealed class CosmosOptions
{
    public string ConnectionString { get; init; }
    public string DatabaseName { get; init; }
    public string ContainerName { get; init; }
}