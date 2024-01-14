using GitIssues.Application.Models;

namespace GitIssues.Application.Clients;

public record CreateIssueItem(string Title, string Description, DateTime CreatedAt, IssueState State, GitIssueAuthorResponse Author);
public record CreateIssueAuthor(string Username, string Url);
public record CreateNewIssue(string Title, string Description);
public record ModifyIssueItem(string Title, string Description, int Id);
public record CloseIssue(IssueState State, int Id);