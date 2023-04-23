namespace Domain.Abstractions;

public interface IRepository
{
}

public interface IRepository<TEntity> : IRepository
    where TEntity : IEntity
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
}