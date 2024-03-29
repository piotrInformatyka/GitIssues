﻿using GitIssues.Application.Clients;
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
        State = item.State == "opened" ? IssueState.Open : IssueState.Closed,
        CreatedAt = item.CreatedAt,
        User = new User
        {
            Login = item.Author.Username,
            WebUrl = item.Author.Url
        },
        RepositoryType = RepositoryType.GitLab
    };

    internal static GitlabCreateIssueItem ToGitlabRequest(this CreateIssueItem issue) => new(issue.Title, issue.Description, issue.CreatedAt, 
        issue.State == IssueState.Open ? "opened" : "closed",
        new GitlabCreateIssueAuthor(issue.Author.Username, issue.Author.Url));

    internal static GitlabCreateNewIssueItem ToGitlabRequest(this CreateNewIssue request) => new(request.Title, request.Description);

    internal static ModifyGitlabItem ToGitlabRequest(this ModifyIssueItem request) => new(request.Title, request.Description, request.Id);

    internal static CloseGitlabIssue ToGitlabRequest(this CloseIssue request) => new(request.State == IssueState.Open ? "opened" : "closed", request.Id);
}
