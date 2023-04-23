using Domain;
using Domain.Abstractions;
using Host.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("[controller]")]
public class TestMassTransitController : ControllerBase
{
    private readonly ILogger<TestCosmosController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public TestMassTransitController(
        ILogger<TestCosmosController> logger,
        IPublishEndpoint publishEndpoint,
        ISendEndpointProvider sendEndpointProvider
    )
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost(Name = "PostTestMassTransit")]
    public async Task Post(CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new TestMessage("test-1", "Some random data"), cancellationToken);

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:test"));
        await sendEndpoint.Send(new TestMessage("test-2", "Blah blah blah"), cancellationToken);
    }
}