namespace GitIssues.Application.Application.Clients.Gitlab;

public record GitIssueItem(string Title, string Description, DateTime CreatedAt, string State, GitIssueAuthor Author);
public record GitIssueAuthor(string Username, string Url);