using GitIssues.Application.Application.Models;

namespace GitIssues.Application.Infrastructure.Clients;

public interface IGitIssueClientStrategy
{
    bool CanBeApplied(RepositoryType repositoryType);
    Task<IEnumerable<Issue>> GetIssuesAsync();
    Task<bool> CreateIssueAsync(Issue issue);
}
