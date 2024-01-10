using System.Text.Json.Serialization;

namespace GitIssues.Application.Infrastructure.Clients.Gitlab;

internal record GitlabClientGetIssuesItem(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("description")] string Body,
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("author")] GitlabClientGetIssuesAuthor Author);

internal record GitlabClientGetIssuesAuthor(
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("web_url")] string Url);