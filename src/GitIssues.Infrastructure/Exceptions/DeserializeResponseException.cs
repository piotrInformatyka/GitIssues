namespace GitIssues.Application.Infrastructure.Exceptions;

internal sealed class DeserializeResponseException : Exception
{
    public DeserializeResponseException(string type, string content) : base($"Can not deserialize object: {type} with content: {content}") { }
}
