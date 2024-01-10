using GitIssues.Application.Application.Models;

namespace GitIssues.Application.Infrastructure.Clients.Github;

internal static class GithubClientMappings
{
    internal static Issue ToIssue(this GithubIssueItem item) => new Issue
    {
        Title = item.Title,
        Body = item.Body,
        State = item.State,
        CreatedAt = item.CreatedAt,
        User = new User
        {
            Login = item.User.Login,
            WebUrl = item.User.Url
        },
    };

    internal static GithubCreateIssueItem ToGithubRequest(this Issue issue) => new(
        issue.Title,
        issue.Body, 
        issue.State, 
        issue.CreatedAt, 
        new GithubCreateIssueAuthor(issue.User.Login, issue.User.WebUrl));
}
