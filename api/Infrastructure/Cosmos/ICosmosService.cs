using Microsoft.Azure.Cosmos;

namespace Infrastructure.Cosmos;

public interface ICosmosService
{
    Task<Container> GetContainerAsync(CancellationToken cancellationToken = default);
}