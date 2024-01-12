namespace GitIssues.Application.Exceptions;

public sealed class InvalidRepositoryTypeException : Exception
{
    public InvalidRepositoryTypeException(string repositoryType)
        : base($"Invalid repository type: {repositoryType}")
    {
    }
}
