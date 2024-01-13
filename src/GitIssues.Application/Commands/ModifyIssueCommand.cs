using GitIssues.Application.Clients;

namespace GitIssues.Application.Commands;

public record ModifyIssueCommand(string Title, string Body, string RepositoryType, int IssueId);

public class ModifyIssueCommandHandler
{
    private readonly IEnumerable<IGitIssueClientStrategy> _gitIssueClientStrategies;

    public ModifyIssueCommandHandler(IEnumerable<IGitIssueClientStrategy> gitIssueClientStrategies)
    {
        _gitIssueClientStrategies = gitIssueClientStrategies;
    }

    public async Task<bool> Handle(ModifyIssueCommand command)
    {
        var strategy = _gitIssueClientStrategies.Single(x => x.CanBeApplied(command.RepositoryType));

        var request = new ModifyGitIssueItem(command.Title, command.Body, command.IssueId);

        var result = await strategy.ModifyIssueAsync(request);
        return result;
    }
}
