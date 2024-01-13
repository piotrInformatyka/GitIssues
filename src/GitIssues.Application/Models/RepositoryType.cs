using GitIssues.Application.Exceptions;

namespace GitIssues.Application.Models;

public record RepositoryType
{
    public string Value { get; }
    public static string GitHub = nameof(GitHub);
    public static string GitLab = nameof(GitLab);

    public RepositoryType(string value)
    {
        if (value != GitHub && value != GitLab)
        {
            throw new InvalidRepositoryTypeException(value);
        }
        Value = value;
    }

    public static implicit operator RepositoryType(string value) => new(value);
    public static implicit operator string(RepositoryType repositoryType) => repositoryType.Value;
}
