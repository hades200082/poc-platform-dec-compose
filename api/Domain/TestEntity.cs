using Domain.Abstractions;
using Newtonsoft.Json;

namespace Domain;

public sealed class TestEntity : IEntity
{
    public TestEntity(string? id)
    {
        Id = id;
    }

    [JsonProperty("id")]
    public string? Id { get; }
    
    [JsonProperty("_pk")]
    public string PartitionKey => GetType().Name;
}