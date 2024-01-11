namespace GitIssues.Application.Application.Clients.Gitlab;

public record CreateGitIssueItem(string Title, string Description, DateTime CreatedAt, string State, GitIssueAuthor Author);
public record CreateGitIssueAuthor(string Username, string Url);
public record CreateNewGitIssue(string Title, string Description);
public record ModifyGitIssueItem(string Title, string Description, int Id);