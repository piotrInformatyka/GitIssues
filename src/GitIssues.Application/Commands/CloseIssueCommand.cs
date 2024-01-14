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

    public async Task<bool> Handle(ModifyIssueCommand command)
    {
        var strategy = _gitIssueClientStrategies.Single(x => x.CanBeApplied(command.RepositoryType));

        var request = new ModifyIssueItem(command.Title, command.Body, command.IssueId);

        var result = await strategy.ModifyIssueAsync(request);
        return result;
    }
}
