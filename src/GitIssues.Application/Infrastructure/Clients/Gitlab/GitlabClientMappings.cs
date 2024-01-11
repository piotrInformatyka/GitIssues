using GitIssues.Application.Application.Models;

namespace GitIssues.Application.Infrastructure.Clients.Gitlab;

internal static class GitlabClientMappings
{
    internal static Issue ToIssue(this GitlabClientGetIssuesItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Body = item.Body,
        State = item.State,
        CreatedAt = item.CreatedAt,
        User = new User
        {
            Login = item.Author.Username,
            WebUrl = item.Author.Url
        },
    };

    internal static GitlabCreateIssueItem ToGitlabRequest(this Issue issue) => new GitlabCreateIssueItem(
        issue.Title,
        issue.Body,
        issue.CreatedAt,
        issue.State,
        new GitlabCreateIssueAuthor(issue.User.Login, issue.User.WebUrl));
}
