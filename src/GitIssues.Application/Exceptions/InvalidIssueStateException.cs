namespace GitIssues.Application.Exceptions;

public sealed class InvalidIssueStateException : Exception
{
    public InvalidIssueStateException(string issueState)
        : base($"Provided invalid issue state: {issueState}")
    {
    }
}
