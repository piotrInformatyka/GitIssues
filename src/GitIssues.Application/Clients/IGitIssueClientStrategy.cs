using GitIssues.Application.Models;

namespace GitIssues.Application.Clients;

public interface IGitIssueClientStrategy
{
    bool CanBeApplied(RepositoryType repositoryType);
    Task<IEnumerable<Issue>> GetIssuesAsync();
    Task<bool> CreateNewIssueAsync(CreateNewIssue request);
    Task<bool> CreateIssueAsync(CreateIssueItem request);
    Task<bool> ModifyIssueAsync(ModifyIssueItem request);
    Task<bool> CloseIssueAsync(CloseIssue request);
}
