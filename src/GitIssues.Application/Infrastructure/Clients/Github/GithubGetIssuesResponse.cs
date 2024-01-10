using System.Text.Json.Serialization;

namespace GitIssues.Application.Infrastructure.Clients.Github;
internal record GithubIssueItem(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("body")] string Body, 
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("user")] GithubIssueAuthor User);

internal record GithubIssueAuthor(
    [property: JsonPropertyName("login")] string Login,
    [property: JsonPropertyName("url")] string Url);
