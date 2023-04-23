using Domain.Abstractions;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.Cosmos;

public class CosmosRepository<TEntity> : IRepository<TEntity>
    where TEntity : IEntity
{
    private readonly ICosmosService _cosmosService;

    public CosmosRepository(ICosmosService cosmosService)
    {
        _cosmosService = cosmosService;
    }
    
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var container = await _cosmosService.GetContainerAsync(cancellationToken);
        return (await container.CreateItemAsync(entity, new PartitionKey(entity.PartitionKey), cancellationToken: cancellationToken)).Resource;
    }
}