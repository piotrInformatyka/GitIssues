using GitIssues.Application.Application.Clients.Gitlab;
using GitIssues.Application.Application.Models;
using GitIssues.Application.Infrastructure.Clients.Github;

namespace GitIssues.Application.Application.Clients;

public interface IGitIssueClientStrategy
{
    bool CanBeApplied(RepositoryType repositoryType);
    Task<IEnumerable<Issue>> GetIssuesAsync();
    Task<bool> CreateNewIssueAsync(CreateNewGitIssue request);
    Task<bool> ModifyIssueAsync(ModifyGitIssueItem request);
}
