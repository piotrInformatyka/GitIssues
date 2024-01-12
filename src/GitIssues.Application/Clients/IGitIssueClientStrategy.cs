using GitIssues.Application.Models;

namespace GitIssues.Application.Clients;

public interface IGitIssueClientStrategy
{
    bool CanBeApplied(RepositoryType repositoryType);
    Task<IEnumerable<Issue>> GetIssuesAsync();
    Task<bool> CreateNewIssueAsync(CreateNewGitIssue request);
    Task<bool> CreateIssueAsync(CreateGitIssueItem request);
    Task<bool> ModifyIssueAsync(ModifyGitIssueItem request);
}
