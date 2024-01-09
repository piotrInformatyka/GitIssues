using System.Text.Json.Serialization;

namespace GitIssues.Application.Infrastructure.Clients.Github;
internal record GithubIssueItem(string Title, string Body, string State, GithubIssueAuthor User)
{
    [JsonPropertyName("Created_at")]
    DateTime CreatedAt { get; }
};

internal record GithubIssueAuthor(string Login, string Url);
