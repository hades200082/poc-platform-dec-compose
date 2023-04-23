using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Infrastructure.Cosmos;

internal sealed class CosmosService : ICosmosService
{
    private readonly CosmosClient _client;
    private readonly IHostEnvironment _environment;
    private readonly CosmosOptions _options;
    private static Container? _container;

    public CosmosService(CosmosClient client, IOptions<CosmosOptions> options, IHostEnvironment environment)
    {
        _client = client;
        _environment = environment;
        _options = options.Value;
    }
    
    public async Task<Container> GetContainerAsync(CancellationToken cancellationToken = default)
    {
        if (_container is null)
        {
            var db = (await _client.CreateDatabaseIfNotExistsAsync(_options.DatabaseName,
                cancellationToken: cancellationToken)).Database;
            _container =
                (await db.CreateContainerIfNotExistsAsync(_options.ContainerName, "/_pk",
                    cancellationToken: cancellationToken)).Container;
        }
        
        return _container;
    }
}