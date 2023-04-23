namespace Domain.Abstractions;

public interface IEntity
{
    string? Id { get; }
    string PartitionKey { get; }
}