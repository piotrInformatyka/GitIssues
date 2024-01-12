using GitIssues.Application.Clients;
using GitIssues.Application.Infrastructure.Clients.Github;
using GitIssues.Application.Infrastructure.Clients.Gitlab;
using GitIssues.Application.Models;

namespace GitIssues.Infrastructure.Clients.Gitlab;

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

    internal static GitlabCreateIssueItem ToGitlabRequest(this CreateGitIssueItem issue) => new(issue.Title, issue.Description, issue.CreatedAt, issue.State,
        new GitlabCreateIssueAuthor(issue.Author.Username, issue.Author.Url));

    internal static GithubCreateNewIssueItem ToGitlabRequest(this CreateNewGitIssue request) => new(request.Title, request.Description);

    //internal static ModifyGithubItem ToGitlabRequest(this ModifyGitIssueItem request) => new(request.Title, request.Description, request.Id);


}
