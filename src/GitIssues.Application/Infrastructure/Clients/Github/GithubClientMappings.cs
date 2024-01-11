using GitIssues.Application.Application.Clients.Gitlab;
using GitIssues.Application.Application.Models;

namespace GitIssues.Application.Infrastructure.Clients.Github;

internal static class GithubClientMappings
{
    public static Issue ToIssue(this GithubIssueItem item) => new()
    {
        Id = item.Id,
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

    public static GithubCreateIssueItem ToGithubRequest(this Issue issue) => new(issue.Title, issue.Body, issue.State, issue.CreatedAt,
        new GithubCreateIssueAuthor(issue.User.Login, issue.User.WebUrl));

    public static ModifyGithubItem ToGithubRequest(this ModifyGitIssueItem request) => new(request.Title, request.Description, request.Id);
    
    public static GithubCreateNewIssueItem ToGithubRequest(this CreateNewGitIssue request) => new(request.Title, request.Description);
}
