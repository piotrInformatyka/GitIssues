namespace GitIssues.Infrastructure.Exceptions;

internal sealed class SerializeRequestException : Exception
{
    public SerializeRequestException(string type) : base($"Can not serialize object: {type}") { }
}
