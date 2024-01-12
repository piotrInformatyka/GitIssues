namespace GitIssues.Application.Clients;

public record GitIssueItemResponse(string Title, string Description, DateTime CreatedAt, string State, GitIssueAuthorResponse Author);
public record GitIssueAuthorResponse(string Username, string Url);