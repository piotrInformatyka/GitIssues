﻿using System.Text.Json.Serialization;

namespace GitIssues.Application.Infrastructure.Clients.Gitlab;

internal record GitlabCreateIssueItem(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("description")] string Body,
    [property: JsonPropertyName("created_at")] DateTime? CreatedAt,
    [property: JsonPropertyName("state")] string? State,
    [property: JsonPropertyName("author")] GitlabCreateIssueAuthor? Author);

internal record GitlabCreateIssueAuthor(
    [property: JsonPropertyName("username")] string Username,
       [property: JsonPropertyName("web_url")] string Url);