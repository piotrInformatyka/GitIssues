using GitIssues.Application.Clients;
using GitIssues.Application.Infrastructure.Clients.Github;
using GitIssues.Application.Models;

namespace GitIssues.Infrastructure.Clients.Github;

public static class GithubClientMappings
{
    internal static Issue ToIssue(this GithubIssueItemResponse item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Body = item.Body,
        State = item.State == "open" ? IssueState.Open : IssueState.Closed,
        CreatedAt = item.CreatedAt,
        User = new User
        {
            Login = item.User.Login,
            WebUrl = item.User.Url
        },
        RepositoryType = RepositoryType.GitHub
        
    };

    internal static ModifyGithubItem ToGithubRequest(this ModifyGitIssueItem request) => new(request.Title, request.Description, request.Id);

    internal static GithubCreateNewIssueItem ToGithubRequest(this CreateNewGitIssue request) => new(request.Title, request.Description);

    internal static GithubCreateIssueItem ToGithubRequest(this CreateGitIssueItem item) => new(item.Title, item.Description, 
        item.State == IssueState.Open ? "opened" : "closed", 
        item.CreatedAt,
        new GithubCreateIssueAuthor(item.Author.Username, item.Author.Url));
}
