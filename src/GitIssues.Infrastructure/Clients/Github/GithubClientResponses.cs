using System.Text.Json.Serialization;

namespace GitIssues.Application.Infrastructure.Clients.Github;
internal record GithubIssueItemResponse(
    [property: JsonPropertyName("id ")] int Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("body")] string Body,
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("user")] GithubIssueAuthorResponse User);

internal record GithubIssueAuthorResponse(
    [property: JsonPropertyName("login")] string Login,
    [property: JsonPropertyName("url")] string Url);
