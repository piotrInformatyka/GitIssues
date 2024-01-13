using GitIssues.Application.Exceptions;

namespace GitIssues.Application.Models;

public record IssueState
{
    public string Value { get; }

    public static string Open = nameof(Open);
    public static string Closed = nameof(Closed);

    public IssueState(string value)
    {
        if (value != Open && value != Closed)
        {
            throw new Exception("Invalid issue type");
        }
        Value = value;
    }

    public static implicit operator IssueState(string value) => new(value);
    public static implicit operator string(IssueState repositoryType) => repositoryType.Value;
}
