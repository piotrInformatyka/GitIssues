using GitIssues.Application.Clients;
using GitIssues.Application.Models;

namespace GitIssues.Application.Commands;

public record CloseIssueCommand(IssueState State, string RepositoryType, int IssueId);

public class CloseIssueCommandHandler
{
    private readonly IEnumerable<IGitIssueClientStrategy> _gitIssueClientStrategies;

    public CloseIssueCommandHandler(IEnumerable<IGitIssueClientStrategy> gitIssueClientStrategies)
    {
        _gitIssueClientStrategies = gitIssueClientStrategies;
    }

    public async Task<bool> Handle(CloseIssueCommand command)
    {
        var strategy = _gitIssueClientStrategies.Single(x => x.CanBeApplied(command.RepositoryType));

        var request = new CloseIssue(command.State, command.IssueId);

        var result = await strategy.CloseIssueAsync(request);
        return result;
    }
}
