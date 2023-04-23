namespace Host.Models;

internal sealed class TestMessage
{
    public TestMessage(string id, string message)
    {
        Id = id;
        Message = message;
    }

    public string Id { get; }
    public string Message { get; }
}