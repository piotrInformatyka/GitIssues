using System.Text.Json.Serialization;

namespace GitIssues.Application.Infrastructure.Clients.Github;

internal record GithubCreateNewIssueItem(
       [property: JsonPropertyName("title")] string Title,
       [property: JsonPropertyName("body")] string Body);

internal record GithubCreateIssueItem(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("body")] string Body,
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("user")] GithubCreateIssueAuthor User);

internal record GithubCreateIssueAuthor(
    [property: JsonPropertyName("login")] string Login,
    [property: JsonPropertyName("url")] string Url);

internal record ModifyGithubItem(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("body")] string Description, 
    int Id);

internal record CloseGithubIssue(
    [property: JsonPropertyName("state")] string State,
    int Id);
