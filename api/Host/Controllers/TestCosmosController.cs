using Domain;
using Domain.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("[controller]")]
public class TestCosmosController : ControllerBase
{
    private readonly ILogger<TestCosmosController> _logger;
    private readonly IRepository<TestEntity> _repository;

    public TestCosmosController(
        ILogger<TestCosmosController> logger,
        IRepository<TestEntity> repository
    )
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost(Name = "PostTestCosmos")]
    public async Task<TestEntity> Post(CancellationToken cancellationToken = default)
    {
        return await _repository.CreateAsync(new TestEntity(Guid.NewGuid().ToString()), cancellationToken);
    }
}